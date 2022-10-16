using FluentValidation;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Queries.Login;

/// <summary>
/// Query to check login
/// </summary>
public record CheckLoginExistsQuery(string Login) : IRequest<bool>
{
    // ReSharper disable once UnusedType.Global
    /// <inheritdoc />
    public class Validator : AbstractValidator<CheckLoginExistsQuery>
    {
        /// <inheritdoc />
        public Validator()
        {
            RuleFor(x => x.Login)
                .SetValidator(new LoginValidator<CheckLoginExistsQuery>());
        }
    }
}