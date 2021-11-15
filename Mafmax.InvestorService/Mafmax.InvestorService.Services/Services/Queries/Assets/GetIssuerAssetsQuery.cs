using FluentValidation;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Queries.Assets;

/// <summary>
/// Query to get all issuer assets
/// </summary>
public record GetIssuerAssetsQuery(int IssuerId)
    : IRequest<ShortAssetDto[]>
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public class Validator : AbstractValidator<GetIssuerAssetsQuery>
    {
        /// <inheritdoc />
        public Validator()
        {
            RuleFor(x => x.IssuerId)
                .SetValidator(new IdValidator<GetIssuerAssetsQuery>("Issuer Id"));
        }
    }
}