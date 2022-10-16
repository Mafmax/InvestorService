using FluentValidation;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Queries.Assets;

/// <summary>
/// Query to get asset by ISIN
/// </summary>
public record GetAssetByIsinQuery(string Isin) : IRequest<AssetDto?>
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global 
    public class Validator : AbstractValidator<GetAssetByIsinQuery>
    {
        /// <inheritdoc />
        public Validator()
        {
            RuleFor(x => x.Isin)
                .SetValidator(new IsinValidator<GetAssetByIsinQuery>());
        }
    }
}