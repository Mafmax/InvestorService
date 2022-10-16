using FluentValidation;

namespace Mafmax.InvestorService.Services.Queries.Assets;

/// <summary>
/// Query to find assets
/// </summary>
public record FindAssetsWithClassQuery(string SearchString,
        string AssetsClass,
        int MinimalSearchStringLength)
    : FindAssetsQuery(SearchString, MinimalSearchStringLength)
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public class ValidatorWithClass : AbstractValidator<FindAssetsWithClassQuery>
    {
        /// <inheritdoc />
        public ValidatorWithClass()
        {
            RuleFor(x => x.AssetsClass)
                .NotEmpty()
                .WithMessage("Assets class name must be not empty");
        }
    }
}