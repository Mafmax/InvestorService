using System;
using System.Threading.Tasks;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Commands.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Mafmax.InvestorService.Services.Services.Commands;

/// <summary>
/// Class to execute commands
/// </summary>
public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _services;

    /// <summary>
    /// Creates dispatcher
    /// </summary>
    /// <param name="services"></param>
    public CommandDispatcher(IServiceProvider services)
    {
        _services = services;
    }

    /// <summary>
    /// Executes command
    /// </summary>
    /// <returns>Result of operation</returns>
    public async Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command)
    {
        if (command is null) throw new ArgumentNullException(nameof(command));

        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));

        var handler = _services.GetRequiredService(handlerType);

        if (handler is null) throw new CommandsHandlerNotFoundException(command.GetType(), typeof(TResult));

        var executeAsyncTask = (Task<TResult>?)handler
            .GetType()
            .GetMethod(nameof(ICommandHandler<ICommand<TResult>, TResult>.ExecuteAsync),new[]{command.GetType()})
            ?.Invoke(handler, new object?[] {command});

        if (executeAsyncTask is null) throw new Exception("Could not load command handler method");

        return await executeAsyncTask;
    }
}