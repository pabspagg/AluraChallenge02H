using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Challenge02.ServicesExtensions
{
    public static class SwaggerExtension
    {
        public static void SwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                options => {
                    options.SwaggerDoc(
                        "v1",
                        new OpenApiInfo
                        {
                            Title = "Financial Control",
                            Version = "v1",
                            Description =
                                "Api em C# com PostgreSql que cumpre os requisitos do Alura Challenge Backend 02. "
                                + "\n Utilizar o cadastro/login e o token gerado para autorizar o acesso aos endpoints. Clicar em Authorize para adicionar o token gerado no login."
                        }
                    );
                    options.AddSecurityDefinition(
                        "Bearer",
                        new OpenApiSecurityScheme()
                        {
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer",
                            BearerFormat = "JWT",
                            In = ParameterLocation.Header,
                            Description =
                                "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                        }
                    );
                    options.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] { }
                            }
                        }
                    );
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);
                }
            );
        }
    }
}
