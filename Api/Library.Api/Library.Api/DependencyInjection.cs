using Library.Application;
using Library.Application.Common.Interfaces;

namespace Library.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services) {
         services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddValidation()
            .AddMediator(); ;
        return services;
    }

    public static IServiceCollection AddValidation(this IServiceCollection services) {
        services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(typeof(AssemplyMarker).Assembly);
        return services;
    }

    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(AssemplyMarker).Assembly);
        });
        return services;
    }
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUser,currentuser>
        services.AddHttpContextAccessor();
        return services;
    }
    public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
    {
        return builder
            .UseHttpsRedirection()
            .UseStatusCodePages()
            .UseAuthentication()
            .UseAuthorization();
    }

}
