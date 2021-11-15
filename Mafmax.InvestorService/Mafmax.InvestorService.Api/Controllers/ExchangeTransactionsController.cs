using System.Threading;
using System.Threading.Tasks;
using Mafmax.InvestorService.Api.Controllers.Base;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.DTOs.RequestDTOs.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mafmax.InvestorService.Api.Controllers;

/// <summary>
/// Provides API to work with transactions
/// </summary>
[ApiController]
[Route("api/transactions")]
public class ExchangeTransactionsController : InvestorServiceControllerBase
{
    ///<inheritdoc/>
    public ExchangeTransactionsController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Creates new transaction into investor portfolio.
    /// </summary>
    /// <returns>Created transaction</returns>
    /// <responce code="201">Returns created transaction</responce>
    /// <responce code="400">If data is incorrect</responce>
    /// <responce code="401">If investor unauthorized</responce>
    /// <responce code="404">If portfolio not found</responce>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [HttpPost("add")]
    public async Task<ActionResult<ExchangeTransactionDto>> AddTransactionIntoPortfolio(
        [FromQuery] AddExchangeTransactionCommandRequestDto commandDto, CancellationToken token)
    {
        var id = await GetCurrentInvestorIdAsync(token);

        return await Mediator.Send(commandDto.GetCommand(id), token);
    }

    /// <summary>
    /// Deletes transaction from investor portfolio.
    /// </summary>
    /// <returns>Status of action</returns>
    /// <responce code="200">If success</responce>
    /// <responce code="400">If transaction not deleted</responce>
    /// <responce code="401">If investor unauthorized</responce>
    /// <responce code="404">If portfolio or transaction not found</responce>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveTransactionFromPortfolio(
        [FromQuery] RemoveExchangeTransactionCommandRequestDto command, CancellationToken token)
    {
            var id = await GetCurrentInvestorIdAsync(token);

            await Mediator.Send(command.GetCommand(id), token);

            return Ok();
    }
}