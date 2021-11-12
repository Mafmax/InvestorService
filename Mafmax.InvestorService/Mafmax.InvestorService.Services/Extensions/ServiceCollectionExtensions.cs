using System;
using System.Collections.Generic;
using System.Linq;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Services.Services.Commands.Interfaces;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mafmax.InvestorService.Services.Extensions;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{

    /// <summary>
    /// Injects main context into container
    /// </summary>
    public static void ConfigureDbContext(this IServiceCollection services, string connectionString)
    {
        var migrationAssemblyName = typeof(InvestorDbContext).Assembly
            .GetName().Name + ".Migrations";

        services.AddDbContext<InvestorDbContext>(opt =>
            opt.UseSqlServer(connectionString, x =>
                x.MigrationsAssembly(migrationAssemblyName)));
    }

    /// <summary>
    /// Injects mapper into container
    /// </summary>
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
    }

    /// <summary>
    /// Injects query handlers into container
    /// </summary>
    public static void AddQueryHandlersScoped(this IServiceCollection services)
    {
        services.AddHandlersScoped(typeof(IQueryHandler<,>));
    }

    /// <summary>
    /// Injects command handlers into container
    /// </summary>
    public static void AddCommandHandlersScoped(this IServiceCollection services)
    {
        services.AddHandlersScoped(typeof(ICommandHandler<,>));
    }

    private static void AddHandlersScoped(this IServiceCollection services, Type type)
    {
        foreach (var handlerType in GetAllImplementedOpenInterfaceTypes(type))
        {
            foreach (var interfaceType in handlerType.GetInterfaces())
            {
                if (interfaceType.GetGenericTypeDefinition() != type) continue;
                services.AddScoped(interfaceType, handlerType);
            }
        }
    }

    private static IEnumerable<Type> GetAllImplementedOpenInterfaceTypes(Type openInterfaceType)
    {
        return openInterfaceType.Assembly
            .GetTypes()
            .Where(x => !x.IsAbstract && !x.IsInterface && x.GetInterfaces().Any(y =>
                y.IsGenericType && y.GetGenericTypeDefinition() == openInterfaceType));
    }
}