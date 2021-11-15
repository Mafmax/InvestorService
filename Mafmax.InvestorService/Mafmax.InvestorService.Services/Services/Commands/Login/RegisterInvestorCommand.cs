using FluentValidation;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Commands.Login;

/// <summary>
/// Command to register new investor
/// </summary>
public record RegisterInvestorCommand(string Login, string Password)
    : IRequest<int>
{
    // ReSharper disable once UnusedType.Global
    /// <inheritdoc />
    public class Validator : AbstractValidator<RegisterInvestorCommand>
    {
        /// <inheritdoc />
        public Validator()
        {
            RuleFor(x => x.Login)
                .SetValidator(new LoginValidator<RegisterInvestorCommand>());

            RuleFor(x => x.Password)
                .SetValidator(new PasswordValidator<RegisterInvestorCommand>());
        }
    }
}