using FluentValidation;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Queries.Portfolios;

/// <summary>
/// Query to get all portfolios of investor
/// </summary>
public record GetAllPortfoliosQuery(int InvestorId) : IRequest<PortfolioShortInfoDto[]>
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public class Validator : AbstractValidator<GetAllPortfoliosQuery>
    {
        /// <inheritdoc />
        public Validator()
        {
            RuleFor(x => x.InvestorId)
                .SetValidator(new IdValidator<GetAllPortfoliosQuery>("Investor Id"));
        }
    }
}