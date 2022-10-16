using FluentValidation;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Queries.Portfolios;

/// <summary>
/// Query to get detailed portfolio info from investor
/// </summary>
public record GetDetailedPortfolioQuery(int InvestorId, int PortfolioId) : IRequest<PortfolioDetailedInfoDto>
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public class Validator : AbstractValidator<GetDetailedPortfolioQuery>
    {
        /// <inheritdoc />
        public Validator()
        {
            RuleFor(x => x.InvestorId)
                .SetValidator(new IdValidator<GetDetailedPortfolioQuery>("Investor Id"));

            RuleFor(x => x.PortfolioId)
                .SetValidator(new IdValidator<GetDetailedPortfolioQuery>("Portfolio Id"));
        }
    }
}