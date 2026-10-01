namespace Library.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services) {
         services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddValidation()
            .AddIdentityInfrastructure()
            .AddMediator() ;
        return services;
    }

    public static IServiceCollection AddValidation(this IServiceCollection services) {
        services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(typeof(AssemplyMarker).Assembly)
            .AddValidatorsFromAssembly(typeof(BookReq).Assembly);
        return services;
    }
    //public static IServiceCollection AddOutputCaching(this IServiceCollection services)
    //{
    //  return  services.AddOutputCache(
    //        options =>
    //        {
    //            options.SizeLimit = 10 * 1024 * 1024;
    //            options.AddBasePolicy(policy =>
    //            {
    //                policy.Expire(TimeSpan.FromSeconds(60));
    //            });
    //        }
    //        );
    //}
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
        services.AddScoped<IUser, CurrentUser>();
        services.AddHttpContextAccessor();
        return services;
    }
    public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
    {
        return builder
            .UseHttpsRedirection()
            .UseStatusCodePages()
            .UseAuthentication()
            .UseAuthorization()
            
            .UseCors("LibraryApp");
    }

}
