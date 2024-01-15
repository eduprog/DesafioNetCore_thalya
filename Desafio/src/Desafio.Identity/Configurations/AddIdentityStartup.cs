using Desafio.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Desafio.Identity;

public static class AddIdentityStartup
{
    public static void AddIdentitySetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultIdentity<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        //JWT
        var jwtAppSettingsOptions = configuration.GetSection(key: "JwtOptions");
        services.Configure<JwtOptions>(jwtAppSettingsOptions);

        var jwtOptions = jwtAppSettingsOptions.Get<JwtOptions>();
        var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = jwtOptions.ValidIn,
                ValidIssuer = jwtOptions.Sender
            };
        });
    }
}
