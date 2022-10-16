using FluentValidation;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Queries.Login;

/// <summary>
/// Query to check credentials
/// </summary>
public record CheckCredentialsQuery(string Login, string Password) : IRequest<bool>
{
    // ReSharper disable once UnusedType.Global
    /// <inheritdoc />
    public class Validator : AbstractValidator<CheckCredentialsQuery>
    {
        /// <inheritdoc />
        public Validator()
        {
            RuleFor(x => x.Login)
                .SetValidator(new LoginValidator<CheckCredentialsQuery>());

            RuleFor(x => x.Password)
                .SetValidator(new PasswordValidator<CheckCredentialsQuery>());
        }
    }
}