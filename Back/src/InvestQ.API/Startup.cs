using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Repositories;
using InvestQ.Domain.Interfaces.Repositories;
using InvestQ.Domain.Interfaces.Services;
using InvestQ.Domain.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace InvestQ.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<InvestQContext>(
                context => context.UseSqlite(_configuration.GetConnectionString("Default"))
            );

            services.AddControllers()
                .AddNewtonsoftJson(
                    opt => opt.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddScoped<IClienteRepo, ClienteRepo>();
            services.AddScoped<ICorretoraRepo, CorretoraRepo>();

            services.AddScoped<IGeralRepo, GeralRepo>();

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<ICorretoraService, CorretoraService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InvestQ.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InvestQ.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
