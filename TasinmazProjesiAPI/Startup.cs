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
using TasinmazProjesiAPI.Business.Abstract;
using TasinmazProjesiAPI.Business.Concrete;
using TasinmazProjesiAPI.DataAccess;

namespace TasinmazProjesiAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            // Veritabaný Baðlantýsý (PostgreSQL)
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // Dependency Injection: Servislerin kaydý
            services.AddScoped<IIlService, IlService>();
            services.AddScoped<IIlceService, IlceService>(); 
            services.AddScoped<IMahalleService, MahalleService>();
            services.AddScoped<ITasinmazService, TasinmazService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", builder =>
                {
                    builder.WithOrigins("http://localhost:44300") // Angular uygulamasýnýn adresi
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
            // Swagger Ayarlarý
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Backend",
                    Version = "v1",
                });
            });

            // Kontrolcülerin kaydý
            services.AddControllers();
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseCors("AllowAngular");
            // Swagger'ý aktifleþtir
            app.UseSwagger();

            // Swagger UI'ý aktifleþtir
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend");
                c.RoutePrefix = "swagger";  // Swagger UI'ýn "/swagger" adresinden eriþilebilir olmasý için
            });

            // Ana sayfayý "/swagger" sayfasýna yönlendirme
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/swagger");
                    return;
                }
                await next();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
