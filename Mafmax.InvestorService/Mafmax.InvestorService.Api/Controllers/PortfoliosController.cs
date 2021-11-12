using System;
using System.Threading.Tasks;
using Mafmax.InvestorService.Api.Controllers.Base;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.DTOs.RequestDTOs.Commands;
using Mafmax.InvestorService.Services.DTOs.RequestDTOs.Queries;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Commands.Interfaces;
using Mafmax.InvestorService.Services.Services.Commands.Portfolios;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;
using Mafmax.InvestorService.Services.Services.Queries.Portfolios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mafmax.InvestorService.Api.Controllers;

/// <summary>
/// Provides API to work with portfolios
/// </summary>
[ApiController]
[Route("api/portfolios")]
public class PortfoliosController : InvestorServiceControllerBase
{
    private const int PortfoliosCountLimit = 3;
    /// <inheritdoc />
    public PortfoliosController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, ILogger<PortfoliosController> logger) : base(queryDispatcher, commandDispatcher, logger)
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
        [FromQuery] CreatePortfolioCommandRequestDto commandDto)
    {
        Response.StatusCode = StatusCodes.Status201Created;

        try
        {
            var id = await GetCurrentInvestorIdAsync();

            return await CommandDispatcher.ExecuteAsync(commandDto.GetCommand(id, PortfoliosCountLimit));
        }
        catch (InvalidOperationException ex)
        {
            LogInformation(ex);
            return BadRequest(ex.Message);
        }
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
        [FromQuery] DeletePortfolioCommandRequestDto commandDto)
    {
        try
        {
            var id = await GetCurrentInvestorIdAsync();

            await CommandDispatcher.ExecuteAsync(commandDto.GetCommand(id));
        }
        catch (EntityNotFoundException ex)
        {
            LogInformation(ex);
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            LogInformation(ex);
            return BadRequest(ex.Message);
        }

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
        [FromQuery] ChangePortfolioCommand command)
    {
        try
        {
            return await CommandDispatcher.ExecuteAsync(command);
        }
        catch (InvalidOperationException ex)
        {
            LogInformation(ex);
            return BadRequest(ex.Message);
        }
        catch (EntityNotFoundException ex)
        {
            LogInformation(ex);
            return NotFound(ex.Message);
        }
    }

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
        [FromQuery] GetAllPortfoliosQuery query)
    {
        try
        {
            return await QueryDispatcher.AskAsync(query);
        }
        catch (EntityNotFoundException ex)
        {
            LogInformation(ex);
            return NotFound(ex.Message);
        }
    }

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
        [FromQuery] GetDetailedPortfolioQueryRequestDto queryDto)
    {
        try
        {
            var id = await GetCurrentInvestorIdAsync();
            
            return await QueryDispatcher.AskAsync(queryDto.GetQuery(id));
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}