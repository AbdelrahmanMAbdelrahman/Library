using Library.Infrastructure.RealTime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration).AddPresentation();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware();

app.MapControllers();
await app.InitializeAsync();
app.MapHub<CopyReturnHub>(CopyReturnHub.URL);
app.Run();
