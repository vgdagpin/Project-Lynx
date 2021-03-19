using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using Lynx.Application;
using Lynx.Infrastructure;
using Lynx.Interfaces;
using Lynx.WebAPI.Common;
using Lynx.Common;
using Microsoft.Extensions.Logging.Console;
using Microsoft.EntityFrameworkCore;
using Lynx.WebAPI.Middlewares;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Lynx.Infrastructure.Common;
using TasqR;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Lynx.DbMigration.SqlServer;

namespace Lynx.WebAPI
{
    public class Startup
    {
        static readonly ILoggerFactory SampleLoggingFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        public Startup(IConfiguration configuration)
        {
            //Configuration = configuration;
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            Configuration = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructureUseSqlServer(Configuration, SampleLoggingFactory);
            services.AddInfrastructure(Configuration);

            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddScoped<IAppUser, AppUser>();
            services.AddScoped<IDataSecure, DataSecure>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IDateTime, AppDateTime>();

            services.AddLogging();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(j =>
            {
                j.RequireHttpsMetadata = false;
                j.SaveToken = true;
                j.TokenValidationParameters = TokenValidationParameters(Configuration);
            });

            services.AddScoped<IJwtSignInManager, JwtSignInManager>();

           
            services.AddControllers()
                .AddJsonOptions(opt => 
                {
                    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lynx.WebAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Lynx.WebAPI v1");
                    options.DefaultModelsExpandDepth(-1);
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<MockDelayResponseMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }



        //JWT Token Validation
        public static TokenValidationParameters TokenValidationParameters(IConfiguration configuration)
        {
            string _tokenValue = configuration["Jwt:Key"],
               issuer = configuration["Jwt:Issuer"];

            if (string.IsNullOrWhiteSpace(_tokenValue))
            {
                throw new SecurityTokenException("JWT key is not set");
            }

            var _tokenKey = Encoding.ASCII.GetBytes(_tokenValue);

            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_tokenKey),
                ValidIssuer = issuer,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true
            };
        }
    }
}
