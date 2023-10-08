using Knowledge.Api.Database;
using Knowledge.Api.Repositories;
using Knowledge.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Knowledge
{
    public class Startup
    {
        public const string ConfigSettingPlatformContext = "ConnectionStrings:PlatformContext";
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Knowledge", Version = "v1" });
            });
            services.AddHostedService<UrlStatusBackgroundService>();
            services
                .AddSingleton<GuidService>()
                .AddScoped<UserRepository>()
                .AddScoped<UrlStatusBackgroundService>()
                .TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var str = Configuration[ConfigSettingPlatformContext];
            services
                .AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(Configuration[ConfigSettingPlatformContext]);
                });
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Knowledge v1"));
            //}
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Knowledge v1"));

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
