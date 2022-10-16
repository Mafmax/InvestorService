using System.Threading;
using System.Threading.Tasks;
using Mafmax.InvestorService.Api.Controllers.Base;
using Mafmax.InvestorService.Services.Commands.Portfolios;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.DTOs.RequestDTOs.Commands;
using Mafmax.InvestorService.Services.DTOs.RequestDTOs.Queries;
using Mafmax.InvestorService.Services.Queries.Portfolios;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mafmax.InvestorService.Api.Controllers;

/// <summary>
/// Provides API to work with portfoliosIMediator mediator,
/// </summary>
[ApiController]
[Route("api/portfolios")]
public class PortfoliosController : InvestorServiceControllerBase
{
    private const int PortfoliosCountLimit = 3;
    /// <inheritdoc />
    public PortfoliosController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Creates investors portfolio. Could not create more than 3 portfolios
    /// </summary>
    /// <returns>Created portfolio</returns>
    ///<produce code="201">Returns created portfolio</produce>
    ///<produce code="400">If operation is invalid</produce>
    ///<produce code="401">If user unauthorized</produce>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<PortfolioDetailedInfoDto>> Create(
        [FromQuery] CreatePortfolioCommandRequestDto commandDto, CancellationToken token)
    {
        var id = await GetCurrentInvestorIdAsync(token);

        return await Mediator.Send(commandDto.GetCommand(id, PortfoliosCountLimit), token);
    }

    /// <summary>
    /// Removes investors portfolio. Could not remove last portfolio.
    /// </summary>
    /// <returns>Result of action</returns>
    /// <produce code="200">If success</produce>
    /// <produce code="400">If incorrect operation</produce>
    /// <produce code="401">If unauthorized</produce>
    /// <produce code="404">If portfolio not found</produce>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(
        [FromQuery] DeletePortfolioCommandRequestDto commandDto, CancellationToken token)
    {
        var id = await GetCurrentInvestorIdAsync(token);

        await Mediator.Send(commandDto.GetCommand(id), token);
        
        return Ok();
    }

    /// <summary>
    /// Changes portfolio parameters. Set null to cancel.
    /// </summary>
    /// <returns>Changed portfolio</returns>
    /// <produce code="200">Returns updated portfolio</produce>
    /// <produce code="400">If data is invalid</produce>
    /// <produce code="401">If investor unauthorized</produce>
    /// <produce code="404">If portfolio not found</produce>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [HttpPut("edit")]
    public async Task<ActionResult<PortfolioDetailedInfoDto>> Edit(
        [FromQuery] UpdatePortfolioCommand command, CancellationToken token) =>
        await Mediator.Send(command, token);

    /// <summary>
    /// Gets short portfolios info for the investor
    /// </summary>
    /// <returns>Array of portfolios</returns>
    /// <produce code="200">Returns array of portfolios</produce>
    /// <produce code="401">If investor is unauthorized</produce>
    /// <produce code="404">If investor not found</produce>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [HttpGet("investor")]
    public async Task<ActionResult<PortfolioShortInfoDto[]>> GetShortInvestorPortfolioInfo(
        [FromQuery] GetAllPortfoliosQuery query, CancellationToken token) =>
        await Mediator.Send(query, token);

    /// <summary>
    /// Gets detailed portfolio info
    /// </summary>
    /// <returns>Portfolio with distribution information</returns>
    ///<produce code="200">Returns portfolio with distribution information</produce>
    ///<produce code="401">If investor is unauthorized</produce>
    ///<produce code="404">If portfolio not found</produce>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [HttpGet("detailed")]
    public async Task<ActionResult<PortfolioDetailedInfoDto>> GetDetailedPortfolioInfo(
        [FromQuery] GetDetailedPortfolioQueryRequestDto queryDto, CancellationToken token)
    {
            var id = await GetCurrentInvestorIdAsync(token);
     
            return await Mediator.Send(queryDto.GetQuery(id), token);
    }
}