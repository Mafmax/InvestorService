using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mafmax.InvestorService.Model.Context;
using MediatR;
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
    public static void AddAutoMapper(this IServiceCollection services) => 
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

    /// <summary>
    /// Injects query handlers into container
    /// </summary>
    public static void AddRequestHandlers(this IServiceCollection services) => 
        services.AddRequestHandlers(typeof(IRequestHandler<,>),Assembly.GetExecutingAssembly());

    private static void AddRequestHandlers(this IServiceCollection services, Type type,Assembly assembly)
    {
        var implementations = GetAllImplementedOpenInterfaceTypes(type,assembly);
        foreach (var handlerType in implementations)
        {
            var interfaceTypes = handlerType.GetInterfaces();
            foreach (var interfaceType in interfaceTypes)
            {
                if (interfaceType.GetGenericTypeDefinition() != type) continue;
                services.AddScoped(interfaceType, handlerType);
            }
        }
    }

    private static IEnumerable<Type> GetAllImplementedOpenInterfaceTypes(Type openInterfaceType,Assembly assembly) =>
        assembly
            .GetTypes()
            .Where(x => !x.IsAbstract && !x.IsInterface && x.GetInterfaces().Any(y =>
                y.IsGenericType && y.GetGenericTypeDefinition() == openInterfaceType));
}