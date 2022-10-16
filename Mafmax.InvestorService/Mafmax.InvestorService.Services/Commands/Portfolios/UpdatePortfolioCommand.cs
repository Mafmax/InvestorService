using FluentValidation;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Commands.Portfolios;

/// <summary>
/// Command to change portfolio parameters. 
/// </summary>
public record UpdatePortfolioCommand(int InvestorId,
    int PortfolioId,
    string? NewName,
    string? NewTargetDescription) : IRequest<PortfolioDetailedInfoDto>
{
    /// <summary>
    /// Validator for <see cref="CreatePortfolioCommand"/>
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class CreatePortfolioCommandValidator : AbstractValidator<UpdatePortfolioCommand>
    {
        /// <inheritdoc />
        public CreatePortfolioCommandValidator()
        {
            RuleFor(x => x.InvestorId)
                .SetValidator(new IdValidator<UpdatePortfolioCommand>("Investor Id"));

            RuleFor(x => x.NewName)
                .SetValidator(new PortfolioNameValidator<UpdatePortfolioCommand>())
                .When(x => x.NewName != null);

            RuleFor(x => x.NewTargetDescription)
                .SetValidator(new PortfolioTargetDescriptionValidator<UpdatePortfolioCommand>())
                .When(x => x.NewTargetDescription != null);
        }
    }

}