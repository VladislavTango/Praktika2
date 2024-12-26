using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PraktikaInfrastructure.JWT
{
    public static class JwtAuthenticationService
    {
        public static void JwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "keklik",
                    ValidAudience = "Yabloki",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VQF3Dv69xcmTZqK4g6gepHZYSLSHT9G2"))
                };
            });
        }
    }
}
