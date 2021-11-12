﻿using System.Threading.Tasks;
using Mafmax.InvestorService.Api.Controllers.Base;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Commands.Interfaces;
using Mafmax.InvestorService.Services.Services.Queries.Assets;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;
using Mafmax.InvestorService.Services.Services.Queries.Issuers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mafmax.InvestorService.Api.Controllers;

/// <summary>
/// Provides API to work with issuers
/// </summary>
[ApiController]
[Route("api/issuers")]
public class IssuersController : InvestorServiceControllerBase
{
    /// <inheritdoc />
    public IssuersController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, ILogger<IssuersController> logger) : base(queryDispatcher, commandDispatcher, logger)
    {
    }

    /// <summary>
    /// Gets all issuer companies
    /// </summary>
    /// <returns>Returns list of issuer companies</returns>
    /// <responce code="200">Returns list of issuer companies</responce>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IssuerDto[]>> Get([FromQuery] GetIssuersQuery query)
    {
        return await QueryDispatcher.AskAsync(query);
    }

    /// <summary>
    /// Gets all assets of issuer-company
    /// </summary>
    /// <returns>List of short assets information</returns>
    /// <responce code="200">Returns list of issuers</responce>
    /// <responce code="404">If issuer not found</responce>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("assets/{issuerId}")]
    public async Task<ActionResult<ShortAssetDto[]>> GetAssets([FromQuery] GetIssuerAssetsQuery query)
    {
        try
        {
            return await QueryDispatcher.AskAsync(query);
        }
        catch (EntityNotFoundException ex)
        {
            LogInformation(ex);
            return NotFound();
        }
    }
}