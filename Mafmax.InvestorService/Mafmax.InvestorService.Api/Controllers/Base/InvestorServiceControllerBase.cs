using System;
using System.Threading.Tasks;
using Mafmax.InvestorService.Services.Services.Queries.Investors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mafmax.InvestorService.Api.Controllers.Base;

/// <inheritdoc />
public abstract class InvestorServiceControllerBase : ControllerBase
{
    /// <summary>
    /// Queries and commands resolver
    /// </summary>
    protected readonly IMediator Mediator;

    /// <summary>
    /// Logger
    /// </summary>
    private readonly ILogger _logger;

    /// <inheritdoc />
    protected InvestorServiceControllerBase(IMediator mediator, ILogger logger)
    {
        Mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets current investor id
    /// </summary>
    /// <returns></returns>
    protected async Task<int> GetCurrentInvestorIdAsync()
        => await Mediator.Send(new GetInvestorIdByLogin(User.Identity!.Name!));

    /// <summary>
    /// Logs information level data
    /// </summary>
    /// <param name="exception"></param>
    protected void LogInformation(Exception exception) =>
        _logger.LogInformation(exception, "{msg}", exception.ToString());

}