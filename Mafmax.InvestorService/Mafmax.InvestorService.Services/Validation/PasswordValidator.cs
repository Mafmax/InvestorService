using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace Mafmax.InvestorService.Services.Validation;

/// <inheritdoc />
public class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const int MinLength = 6;
    private const string PropertyName = "Password";

    /// <inheritdoc />
    public override bool IsValid(ValidationContext<T> context, string value)
    {
        var valid = true;

        if (value.Length < MinLength)
        {
            context.AddFailure(PropertyName, $"{PropertyName} length must be greater or equal {MinLength}");
            valid = false;
        }

        if (!Regex.IsMatch(value, "[0-9]"))
        {
            context.AddFailure(PropertyName, $"{PropertyName} must contains at least 1 digit");
            valid = false;
        }

        if (!Regex.IsMatch(value, "[a-z]"))
        {
            context.AddFailure(PropertyName, $"{PropertyName} must contains at least 1 lowercase letter");
            valid = false;
        }

        if (!Regex.IsMatch(value, "[A-Z]"))
        {
            context.AddFailure(PropertyName, $"{PropertyName} must contains at least 1 uppercase letter");
            valid = false;
        }

        return valid;
    }

    /// <inheritdoc />
    public override string Name { get; } = nameof(PasswordValidator<T>);

}
