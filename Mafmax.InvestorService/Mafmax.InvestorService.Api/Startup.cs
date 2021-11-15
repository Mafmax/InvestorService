using System.Reflection;
using Mafmax.InvestorService.Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Mafmax.InvestorService.Services.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Mafmax.InvestorService.Api;

/// <summary>
/// 
/// </summary>
public class Startup
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// 
    /// </summary>
    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureDbContext(Configuration.GetConnectionString("InvestorService"));

        services.AddRequestHandlers();

        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddAutoMapper();

        services.AddTransient<ExceptionHandlingMiddleware>();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opt =>
            {
                opt.LoginPath = new PathString("/api/login/error");

            });

        services.AddControllers();

        services.ConfigureValidation();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mafmax.InvestorService.Api", Version = "v1" });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
            
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mafmax.InvestorService.Api v1"));

        if (env.IsDevelopment()) 
            app.UseDeveloperExceptionPage();

        app.UseSerilogRequestLogging();

        app.UseRouting();

        app.UseAuthentication();
        
        app.UseAuthorization();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}