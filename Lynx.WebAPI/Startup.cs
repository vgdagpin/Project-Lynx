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
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            Configuration = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructureUseSqlServer(Configuration, SampleLoggingFactory);

            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddScoped<IAppUser>(p => new AppUser());

            services.AddControllers()
                .AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lynx.WebAPI", Version = "v1" });
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
