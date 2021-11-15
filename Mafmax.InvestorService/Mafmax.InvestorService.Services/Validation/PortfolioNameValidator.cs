using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace Mafmax.InvestorService.Services.Validation;

/// <inheritdoc />
public class PortfolioNameValidator<T> : PropertyValidator<T, string?>
{
    private const int MinLength = 4;

    private const string PropertyName = "Portfolio name";

    /// <inheritdoc />
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        var valid = true;

        if (value is null)
        {
            context.AddFailure(PropertyName, $"{PropertyName} could not be null");
            return false;
        }

        if (value.Length < MinLength)
        {
            context.AddFailure(PropertyName, $"{PropertyName} length must bee greater than {MinLength}");
            valid = false;
        }

        if (!Regex.IsMatch(value, @"[a-zA-Zа-яА-Я0-9 ,.!]*"))
        {
            context.AddFailure(PropertyName, $"{PropertyName} contains forbidden symbols. Allowed characters, digits, spaces, and special symbols e.g. (.,!)");
            valid = false;
        }

        return valid;
    }

    /// <inheritdoc />
    public override string Name { get; } = nameof(PortfolioNameValidator<T>);
}
