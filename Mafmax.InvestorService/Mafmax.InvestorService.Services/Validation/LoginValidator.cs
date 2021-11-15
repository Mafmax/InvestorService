using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace Mafmax.InvestorService.Services.Validation;

/// <inheritdoc />
public class LoginValidator<T> : PropertyValidator<T, string>
{
    private const int MinLength = 8;
    private const string PropertyName = "Login";

    /// <inheritdoc />
    public override bool IsValid(ValidationContext<T> context, string value)
    {
        var valid = true;

        if (value.Length < MinLength)
        {
            context.AddFailure(PropertyName, $"{PropertyName} length must be greater or equal {MinLength}");
            valid = false;
        }

        if (Regex.IsMatch(value,"^[0-9]"))
        {
            context.AddFailure(PropertyName, $"{PropertyName} could not starts with digit");
            valid = false;
        }

        return valid;
    }

    /// <inheritdoc />
    public override string Name { get; } = nameof(LoginValidator<T>);
}
