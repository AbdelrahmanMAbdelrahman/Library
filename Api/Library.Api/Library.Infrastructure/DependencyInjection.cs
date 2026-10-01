using Library.Infrastructure.BackGroundJobs;
using Library.Infrastructure.Configuration.Options;
using Library.Infrastructure.Data.Interceptor;
using Library.Infrastructure.RealTime;
using Library.Infrastructure.Services;
using Library.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace Library.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddSignalR();
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
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ICopyNotifier, SignalRCopyReturnNotifier>();
        services.AddDbContext<AppDbContext>((sp, options) =>
        {//interceptor later
            
            options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>());
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
                policy
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
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
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/Hubs/CopyNotifier"))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
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
        services.AddHybridCache(options =>
        {
            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(10),
                LocalCacheExpiration = TimeSpan.FromSeconds(30)
            };
        });
        return services;
    }
    

}
