
using Library.Domain.Fines;
using Library.Domain.Fines.Enums;
using Library.Infrastructure.Settings;

namespace Library.Infrastructure.BackGroundJobs
{
    public sealed class FineTrackingService(
       
        ILogger<FineTrackingService>logger,
        IServiceScopeFactory scopeFactory,
        IOptions<AppSettings> options) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(//TimeSpan.FromMinutes(1)
                TimeSpan.FromHours( options.Value.DefaultPeriodicFineCheck)
                );
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                logger.LogInformation("Start Check Fines For borrowing records");
                var scope=scopeFactory.CreateScope();
                AppDbContext context=scope.ServiceProvider.GetRequiredService<AppDbContext>();
                //get borrowing records which has borrowing date and doesn't have actual return date
                List<BorrowingRecord> borrowingRecords=await context.BorrowingRecords
                    .Include(br=>br.AppUser)
                    .Where(br=> br.ActualReturnDate ==null&&br.DueDate<DateTime.UtcNow)
                    .ToListAsync();
                List<Guid> borrowingRecordIds = borrowingRecords.Select(br=>br.Id).ToList();
                //check if fines table already contain fine for this record 
                List<Fine> fines = await context.Fines
                    .Include(f=>f.BorrowingRecord)
                    .Where(
                    f => 
                    borrowingRecordIds.Contains(f.BorrowingRecordId)
                    ).ToListAsync();
                List<BorrowingRecord> BorrowingRecordsWithNoFine =
                    borrowingRecords
                    .Where(
                        br=>!fines.Select(f=>f.BorrowingRecordId).Contains(br.Id)
                        ).ToList();
                //if exist update fine else insert new fine immediatly
                
                foreach (Fine fine in fines)
                {
                    int lateDays =(int) (DateTime.UtcNow-fine.BorrowingRecord.DueDate).TotalDays;
                    double FineAmount = lateDays * options.Value.DefaultFinePerDay;
                    fine.UpdateFine(lateDays,FineAmount,PaymentStatus.UnPaid);
                }
                Result<Fine> fineResult;
                List<Fine> NewFines = new List<Fine>();
                foreach(BorrowingRecord borrowingRecord in BorrowingRecordsWithNoFine)
                {
                    int lateDays =(int) (DateTime.UtcNow - borrowingRecord.DueDate).TotalDays;
                    double FineAmount = lateDays * options.Value.DefaultFinePerDay;
                    
                    fineResult = Fine.Create(borrowingRecord.Id,borrowingRecord.AppUserId,lateDays,FineAmount,PaymentStatus.UnPaid);
                    if (fineResult.IsError)
                    {
                        logger.LogError(string.Join(" - ",fineResult.Errors));
                        continue;
                    }
                    NewFines.Add(fineResult.Value);

                }
                context.Fines.AddRange(NewFines);
                await context.SaveChangesAsync(stoppingToken);
            }
        }
    }
}
