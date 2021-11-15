using FluentValidation;
using FluentValidation.Validators;

namespace Mafmax.InvestorService.Services.Validation;

/// <inheritdoc />
public class IdValidator<T> : PropertyValidator<T, int>
{
    private readonly string _fieldName;

    /// <inheritdoc />
    public IdValidator(string fieldName)
    {
        _fieldName = fieldName;
    }

    /// <inheritdoc />
    public override bool IsValid(ValidationContext<T> context, int value)
    {
        if (value >= 0) return true;
        context.AddFailure(_fieldName, $"{_fieldName} must be greater than 0");
        return false;
    }

    /// <inheritdoc />
    public override string Name { get; } = nameof(IdValidator<T>);

    
}
