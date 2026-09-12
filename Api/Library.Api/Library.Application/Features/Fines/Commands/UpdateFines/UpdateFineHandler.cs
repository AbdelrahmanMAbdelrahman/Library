
namespace Library.Application.Features.Fines.Commands.UpdateFines;

public sealed class UpdateFineHandler() : IRequestHandler<UpdateFineCommand, Result<Updated>>
{
    public Task<Result<Updated>> Handle(UpdateFineCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
