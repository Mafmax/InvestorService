using System;
using System.Threading.Tasks;
using Mafmax.InvestorService.Services.Services.Commands.Interfaces;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;
using Mafmax.InvestorService.Services.Services.Queries.Investors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mafmax.InvestorService.Api.Controllers.Base
{
    /// <inheritdoc />
    public abstract class InvestorServiceControllerBase : ControllerBase
    {
        /// <summary>
        /// Dispatcher to handle queries 
        /// </summary>
        protected readonly IQueryDispatcher QueryDispatcher;

        /// <summary>
        /// Dispatcher to handle commands
        /// </summary>
        protected readonly ICommandDispatcher CommandDispatcher;

        /// <summary>
        /// Logger
        /// </summary>
        protected readonly ILogger Logger;

        /// <inheritdoc />
        protected InvestorServiceControllerBase(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, ILogger logger)
        {
            QueryDispatcher = queryDispatcher;
            CommandDispatcher = commandDispatcher;
            Logger = logger;
        }

        /// <summary>
        /// Gets current investor id
        /// </summary>
        /// <returns></returns>
        protected async Task<int> GetCurrentInvestorIdAsync()
            =>  await QueryDispatcher.AskAsync(new GetInvestorIdByLogin(User.Identity!.Name!));


        /// <summary>
        /// Logs information level data
        /// </summary>
        /// <param name="exception"></param>
        protected void LogInformation(Exception exception)
        {
            Logger.LogInformation(exception,exception.ToString());
        }

        /// <summary>
        /// Logs warning level data
        /// </summary>
        /// <param name="exception"></param>
        protected void LogWarning(Exception exception)
        {
            Logger.LogWarning(exception,exception.ToString());
        }
    }
}
