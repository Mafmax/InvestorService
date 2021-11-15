using FluentValidation;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Commands.ExchangeTransactions;

/// <summary>
/// Command to delete transaction from portfolio
/// </summary>
public record DeleteExchangeTransactionCommand(int InvestorId, int PortfolioId, int TransactionId)
    : IRequest
{
    // ReSharper disable once UnusedType.Global
    /// <inheritdoc />
    public class Validator : AbstractValidator<DeleteExchangeTransactionCommand>
    {
        /// <inheritdoc />
        public Validator()
        {
            RuleFor(x => x.InvestorId)
                .SetValidator(new IdValidator<DeleteExchangeTransactionCommand>("Investor Id"));

            RuleFor(x => x.PortfolioId)
                .SetValidator(new IdValidator<DeleteExchangeTransactionCommand>("Portfolio Id"));

            RuleFor(x => x.TransactionId)
                .SetValidator(new IdValidator<DeleteExchangeTransactionCommand>("Transaction Id"));
        }
    }
}