using FluentValidation;
using Mafmax.InvestorService.Services.DTOs;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Queries.Assets;

/// <summary>
/// Query to find assets
/// </summary>
public record FindAssetsQuery(string SearchString, int MinimalSearchStringLength)
    : IRequest<ShortAssetDto[]>
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public class Validator : AbstractValidator<FindAssetsQuery>
    {
        /// <inheritdoc />
        public Validator()
        {
            RuleFor(x => x.SearchString.Length)
                .GreaterThan(x => x.MinimalSearchStringLength)
                .WithMessage("Search string too short");

            RuleFor(x => x.MinimalSearchStringLength)
                .GreaterThan(0)
                .WithMessage("Minimal search string length must be greater than 0");
        }
    }
}