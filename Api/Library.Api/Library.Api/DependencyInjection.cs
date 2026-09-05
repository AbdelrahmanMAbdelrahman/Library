namespace Library.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services) {
         services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
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
