using Library.Domain.Identity.Roles;
using Library.Domain.Identity.Users;
using Library.Infrastructure.Configuration;
using Library.Infrastructure.Data;
using Library.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        return services.AddSingleton(TimeProvider.System)
            .AddConnectionString(configuration)
            .AddDependencyInjection()
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
    public static IServiceCollection AddJwt(this IServiceCollection services,IConfiguration configuration) {
        JwtOptions? jwtOptions=configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        ArgumentNullException.ThrowIfNull(jwtOptions);

        services.AddIdentityCore<AppUser>();

        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddOptions<JwtOptions>()
            .BindConfiguration(nameof(JwtOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();

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
            .AddScoped<IAppDbContext,AppDbContext>();
        return services;
    }


}
