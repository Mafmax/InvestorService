using System;
using System.Threading.Tasks;
using Mafmax.InvestorService.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mafmax.InvestorService.Services.Extensions
{
    /// <summary>
    /// Extensions fot <see cref="IServiceProvider"/>
    /// </summary>
    public static class ServiceProviderExtensions
    {

        /// <summary>
        /// Migrates context with data seeding
        /// </summary>
        public static async Task MigrateDbAsync(this IServiceProvider services)
        {
            var context = services.GetRequiredService<InvestorDbContext>();

            if (context.Database.IsSqlServer())
                await context.Database.MigrateAsync();
            
            await context.SeedDatabaseAsync();
        }
    }
}
