using FluentValidation;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Commands.Portfolios;

/// <summary>
/// Command to create new portfolio
/// </summary>
public record CreatePortfolioCommand(int InvestorId,
    string Name,
    string TargetDescription,
    int PortfoliosCountLimit = 3) : IRequest<PortfolioDetailedInfoDto>
{
    /// <summary>
    /// Validator for <see cref="CreatePortfolioCommand"/>
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class CreatePortfolioCommandValidator : AbstractValidator<CreatePortfolioCommand>
    {
        /// <inheritdoc />
        public CreatePortfolioCommandValidator()
        {
            RuleFor(x => x.InvestorId)
                .SetValidator(new IdValidator<CreatePortfolioCommand>("Investor Id"));

            RuleFor(x => x.Name)
                .SetValidator(new PortfolioNameValidator<CreatePortfolioCommand>());

            RuleFor(x => x.TargetDescription)
                .SetValidator(new PortfolioTargetDescriptionValidator<CreatePortfolioCommand>());

            RuleFor(x => x.PortfoliosCountLimit)
                .GreaterThan(0)
                .WithMessage("Limit of portfolios must be greater than 0");
        }
    }

}