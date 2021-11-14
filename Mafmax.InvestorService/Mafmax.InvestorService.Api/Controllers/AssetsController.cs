using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mafmax.InvestorService.Api.Controllers.Base;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.DTOs.RequestDTOs.Queries;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Queries.Assets;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Mafmax.InvestorService.Api.Controllers;

/// <summary>
/// Provides api to work with assets
/// </summary>
[ApiController]
[Route("api/assets")]
public class AssetsController : InvestorServiceControllerBase
{
    private const int MinimalSearchStringLength = 3;

    /// <inheritdoc />
    public AssetsController(IMediator mediator, ILogger<AssetsController> logger) : base(mediator, logger)
    {
    }

    /// <summary>
    /// Gets asset by identifier
    /// </summary>
    /// <returns>Asset full information</returns>
    ///<responce code="200">Returns asset</responce>
    ///<responce code="404">If asset not found</responce>
    [HttpGet("find/id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AssetDto>> GetByIdAsync([FromQuery] GetAssetByIdQuery query, CancellationToken token)
    {
        try
        {
            return await Mediator.Send(query, token);
        }
        catch (EntityNotFoundException ex)
        {
            LogInformation(ex);
            return NotFound(ex);
        }
    }

    /// <summary>
    /// Gets asset by identifier
    /// </summary>
    /// <returns>Asset full information</returns>
    ///<responce code="200">Returns asset</responce>
    ///<responce code="404">If asset not found</responce>
    [HttpGet("find/isin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AssetDto>> GetByIsinAsync([FromQuery] GetAssetByIsinQuery query, CancellationToken token)
    {
        try
        {
            return await Mediator.Send(query, token);
        }
        catch (EntityNotFoundException ex)
        {
            LogInformation(ex);
            return NotFound();
        }
    }

    /// <summary>
    /// Finds assets
    /// </summary>
    /// <returns>Found assets of class</returns>
    ///<responce code="200">Returns found assets</responce>
    ///<responce code="400">If search query is invalid</responce>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("search")]
    public async Task<ActionResult<Dictionary<string, ShortAssetDto[]>>> FindAsync(
        [FromQuery] FindAssetsQueryRequestDto queryDto, CancellationToken token)
    {
        try
        {
            return GroupAssetsByClass(
                await Mediator.Send(queryDto.GetQuery(MinimalSearchStringLength), token));
        }
        catch (InvalidOperationException ex)
        {
            LogInformation(ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Finds assets
    /// </summary>
    /// <returns>Found assets grouped by class</returns>
    ///<responce code="200">Returns found assets</responce>
    ///<responce code="400">If search query is invalid</responce>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("search/{assetsClass?}")]
    public async Task<ActionResult<Dictionary<string, ShortAssetDto[]>>> FindAsync(
        [FromQuery] FindAssetsQueryRequestDto queryDto, string assetsClass, CancellationToken token)
    {
        try
        {
            if (assetsClass is null)
                return await FindAsync(queryDto, token);

            return GroupAssetsByClass(
                await Mediator.Send(queryDto.GetQuery(MinimalSearchStringLength, assetsClass), token));
        }
        catch (InvalidOperationException ex)
        {
            LogInformation(ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Gets issuer assets
    /// </summary>
    /// <returns>Array of issuer assets</returns>
    /// <responce code="200">Returns issuer assets</responce>
    /// <responce code="404">If issuer not found</responce>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("issuer")]
    public async Task<ActionResult<ShortAssetDto[]>> GetIssuerAssets([FromQuery] GetIssuerAssetsQuery query, CancellationToken token)
    {
        try
        {
            return await Mediator.Send(query, token);
        }
        catch (EntityNotFoundException ex)
        {
            LogInformation(ex);
            return NotFound(ex.Message);
        }
    }

    private static Dictionary<string, ShortAssetDto[]> GroupAssetsByClass(params ShortAssetDto[] assets)
        => assets
            .GroupBy(x => x.Class)
            .ToDictionary(x => x.Key, x => x.ToArray());
}