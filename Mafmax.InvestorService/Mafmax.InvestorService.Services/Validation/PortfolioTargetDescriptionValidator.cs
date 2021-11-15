using FluentValidation;
using FluentValidation.Validators;

namespace Mafmax.InvestorService.Services.Validation;

/// <inheritdoc />
public class PortfolioTargetDescriptionValidator<T> : PropertyValidator<T, string?>
{

    private const int MinLength = 10;
    private const int MaxLength = 500;
    private const string PropertyName = "Taerget description";

    /// <inheritdoc />
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        var valid = true;

        if (value is null)
        {
            context.AddFailure(PropertyName,$"{PropertyName} could not be null");
            return false;
        }

        switch (value.Length)
        {
            case < MinLength:
                context.AddFailure(PropertyName, $"{PropertyName} length must be greater or equal {MinLength}");
                valid = false;
                break;

            case > MaxLength:
                context.AddFailure(PropertyName, $"{PropertyName} length must be less or equal {MaxLength}");
                valid = false;
                break;
        }

        return valid;
    }

    /// <inheritdoc />
    public override string Name { get; } = nameof(PortfolioTargetDescriptionValidator<T>);
}
