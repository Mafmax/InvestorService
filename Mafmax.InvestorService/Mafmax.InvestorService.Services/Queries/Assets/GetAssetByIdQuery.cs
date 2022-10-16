using FluentValidation;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Queries.Assets;

/// <summary>
/// Query to get asset by id
/// </summary>
public record GetAssetByIdQuery(int Id) : IRequest<AssetDto?>
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public class Validator : AbstractValidator<GetAssetByIdQuery>
    {
        /// <inheritdoc />
        public Validator()
        {
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<GetAssetByIdQuery>("Asset Id"));
        }
    }
}