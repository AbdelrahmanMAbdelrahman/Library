using Library.Infrastructure.BackGroundJobs;
using Library.Infrastructure.Configuration.Options;
using Library.Infrastructure.Services;
using Library.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace Library.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        return services.AddSingleton(TimeProvider.System)
            .AddConnectionString(configuration)
            .AddDependencyInjection()
            .AddCorsService()
            .AddOptions()
            .AddJwt(configuration);

    }
    public static IServiceCollection AddConnectionString(this IServiceCollection services, IConfiguration configuration) {
        string? ConnectionString = configuration.GetConnectionString("Default");
        ArgumentNullException.ThrowIfNullOrWhiteSpace(ConnectionString);
        services.AddDbContext<AppDbContext>((sp, options) =>
        {//interceptor later
            options.UseSqlServer(ConnectionString);
        });
        return services;    
    }
    static IServiceCollection AddCorsService(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("LibraryApp", policy =>
            {
                policy.AllowAnyHeader()
                 .AllowAnyMethod()
                 .WithOrigins(["http://localhost:4200"]);
            });
        });
        return services;
    }
    //public static IServiceCollection AddOptions(this IServiceCollection service,IConfiguration configuration)
    //{
    //    JwtOptions? jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
    //    ArgumentNullException.ThrowIfNull(jwtOptions);

    //    AppSettings? appSettings = configuration.GetSection(nameof(appSettings)).Get<AppSettings>();
    //    ArgumentNullException.ThrowIfNull(appSettings);
    //    return service;
    //}
    public static IServiceCollection AddOptions(this IServiceCollection services)
    {
        services.AddOptions<JwtOptions>()
            .BindConfiguration(nameof(JwtOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddOptions<AppSettings>()
            .BindConfiguration(nameof(AppSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        return services;
    }
    public static IServiceCollection AddJwt(this IServiceCollection services,IConfiguration configuration) {

        JwtOptions? jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        ArgumentNullException.ThrowIfNull(jwtOptions);

        
        services.AddIdentityCore<AppUser>();

        services.AddIdentity<AppUser,IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                ValidIssuer =jwtOptions.Issuer,
                ValidAudience=jwtOptions.Audience,
                ClockSkew=TimeSpan.Zero
            };
        });
        
        return services;
    }
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services) {
        services.AddScoped<IIdentityService, IdentityService>()
            .AddScoped<IAppDbContext, AppDbContext>()
            .AddScoped<ITokenProvider, TokenProvider>()
            .AddScoped<IFileStorage, FileStorageService>()
            .AddScoped<AppDbContextInitializer>()
            .AddHostedService<FineTrackingService>();

        return services;
    }
    

}
