using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace Mafmax.InvestorService.Services.Validation;

/// <inheritdoc />
public class IsinValidator<T> : PropertyValidator<T,string?>
{
    private const string PropertyName = "ISIN";

    /// <inheritdoc />
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (value is null)
        {
            context.AddFailure(PropertyName,$"{PropertyName} could not be null");
            return false;
        }

        if (Regex.IsMatch(value, "[a-zA-Z]{2}[0-9a-zA-Z]{9}[0-9]{1}")) 
            return true;

        context.AddFailure(PropertyName, $"{PropertyName} must matches the pattern: 2 digits, 9 digits/symbols, 1 control digit");
        return false;
    }

    /// <inheritdoc />
    public override string Name { get; } = nameof(IsinValidator<T>);
}
