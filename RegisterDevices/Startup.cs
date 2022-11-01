using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Polly;
using RegisterDevices.Models;
using RegisterDevices.Repository;
using RegisterDevices.Services;

namespace RegisterDevices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDeviceRepository, DeviceRepository>();

            services.AddDbContext<DeviceContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DeviceDatabase")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RegisterDevicesAPI", Version = "v1" });
            });
            services.AddTransient<IGetInventoryIdService, GetInventoryIdService>();
            ConfigureInventoryApi(services);
        }

        private void ConfigureInventoryApi(IServiceCollection services)
        {
            var inventoryApiConfig = Configuration.GetSection(nameof(InventoryApi)).Get<InventoryApi>();
            services.AddSingleton(inventoryApiConfig);

            services
                .AddHttpClient(GetInventoryIdService.HttpClientName )
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(2, _ => TimeSpan.FromMilliseconds(600)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RegisterDevicesAPI v1"));
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
