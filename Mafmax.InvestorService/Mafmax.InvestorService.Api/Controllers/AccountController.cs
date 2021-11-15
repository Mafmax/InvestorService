using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Mafmax.InvestorService.Api.Controllers.Base;
using Mafmax.InvestorService.Services.Services.Commands.Login;
using Mafmax.InvestorService.Services.Services.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mafmax.InvestorService.Api.Controllers;

/// <summary>
/// Provides API to work with investor accounts
/// </summary>
[ApiController]
[Route("api/account")]
public class AccountController : InvestorServiceControllerBase
{
    /// <inheritdoc />
    public AccountController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Register new investor
    /// </summary>
    /// <returns>Id of created investor</returns>
    /// <produce code="400">If user with same login already exists</produce>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("register")]
    public async Task<ActionResult<int>> Register([FromQuery] RegisterInvestorCommand command, CancellationToken token)
    {
        CheckLoginExistsQuery query = new(command.Login);

        if (!await Mediator.Send(query, token))
            return await Mediator.Send(command, token);

        return BadRequest($"User with login = \"{command.Login}\" already exists");
    }

    /// <summary>
    /// Sign in investor
    /// </summary>
    /// <returns>Result of action</returns>
    /// <produce code="200">If success</produce>
    /// <produce code="401">If incorrect credentials</produce>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromQuery] CheckCredentialsQuery query, CancellationToken token)
    {
        if (!await Mediator.Send(query, token))
            return Unauthorized("Invalid login or password");

        await Authenticate(query.Login);

        return Ok();
    }

    /// <summary>
    /// Shows unauthorized error
    /// </summary>
    [HttpGet("error")]
    public IActionResult LoginErrorPage() =>
        Unauthorized("Not authorized");

    private async Task Authenticate(string login)
    {
        var claims = new[]
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, login)
        };

        var id = new ClaimsIdentity(claims,
            authenticationType: "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    /// <summary>
    /// Exit
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpPost("logout")]
    public async Task Logout() =>
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
}