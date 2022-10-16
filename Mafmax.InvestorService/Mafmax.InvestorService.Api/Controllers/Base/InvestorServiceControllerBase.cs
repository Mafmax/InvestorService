using System.Threading;
using System.Threading.Tasks;
using Mafmax.InvestorService.Services.Queries.Investors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mafmax.InvestorService.Api.Controllers.Base;

/// <inheritdoc />
public abstract class InvestorServiceControllerBase : ControllerBase
{
    /// <summary>
    /// Queries and commands resolver
    /// </summary>
    protected readonly IMediator Mediator;

    /// <inheritdoc />
    protected InvestorServiceControllerBase(IMediator mediator)
    {
        Mediator = mediator;
    }

    /// <summary>
    /// Gets current investor id
    /// </summary>
    /// <returns></returns>
    protected async Task<int> GetCurrentInvestorIdAsync(CancellationToken token)
        => await Mediator.Send(new GetInvestorIdByLogin(User.Identity!.Name!),token);
}