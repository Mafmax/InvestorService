using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Mafmax.InvestorService.Api;
using Mafmax.InvestorService.Services.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

var host = CreateHostBuilder(args).Build();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs.txt")
    .CreateLogger();

using (var scope = host.Services.CreateScope())
    await scope.ServiceProvider.MigrateDbAsync();

await host.RunAsync();
    

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseUrls("http://*:80");
            webBuilder.UseStartup<Startup>();
        });

