using Library.Api;
using Library.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Library.Application.SubCutaneousTests.Common;
public class WebAppFactory : WebApplicationFactory<IAssemblyMarker>, IAsyncLifetime
{
    private readonly MsSqlContainer dbContainer=new MsSqlBuilder().Build();

    public IMediator CreateMediator()
    {
        var scope = Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<IMediator>();
    }
    public IAppDbContext CreateDbContext()
    {
        var scope = Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<IAppDbContext>();
    }
    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        throw new NotImplementedException();
    }
}
