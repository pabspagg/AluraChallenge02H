using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Challenge02.ServicesExtensions
{
    public static class AuthenticationExtension
    {
        public static void AuthenticationService(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services
                .AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer(
                    options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(
                                    config.GetValue<String>("JwtToken:SecretKey")
                                )
                            ),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    }
                );
        }
    }
}
