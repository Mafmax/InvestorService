using FluentValidation;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Commands.Portfolios;

/// <summary>
/// Command to delete portfolio
/// </summary>
public record DeletePortfolioCommand(int InvestorId, int PortfolioId) : IRequest
{
    // ReSharper disable once UnusedType.Global
    /// <inheritdoc />
    public class Validator : AbstractValidator<DeletePortfolioCommand>
    {
        /// <inheritdoc />
        public Validator()
        {
            RuleFor(x => x.InvestorId)
                .SetValidator(new IdValidator<DeletePortfolioCommand>("Investor Id"));

            RuleFor(x => x.PortfolioId)
                .SetValidator(new IdValidator<DeletePortfolioCommand>("Portfolio Id"));
        }
    }
}