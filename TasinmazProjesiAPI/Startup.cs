using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TasinmazProjesiAPI.Business.Abstract;
using TasinmazProjesiAPI.Business.Concrete;
using TasinmazProjesiAPI.DataAccess;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // JWT Key
        var key = Encoding.ASCII.GetBytes(Configuration["AppSettings:Token"]);
        if (key == null || key.Length < 16)
        {
            throw new Exception("AppSettings:Token deðeri geçersiz. En az 16 karakter uzunluðunda olmalýdýr.");
        }

        // Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        // Veritabaný baðlantýsý
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        // Dependency Injection
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<ILogService, LogService>();
        services.AddScoped<IIlService, IlService>();
        services.AddScoped<IIlceService, IlceService>();
        services.AddScoped<IMahalleService, MahalleService>();
        services.AddScoped<ITasinmazService, TasinmazService>();

        // CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngular", builder =>
            {
                builder.WithOrigins("http://localhost:44300")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        // Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend", Version = "v1" });
        });

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
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend");
            c.RoutePrefix = "swagger";
        });

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication(); // JWT Authentication
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
