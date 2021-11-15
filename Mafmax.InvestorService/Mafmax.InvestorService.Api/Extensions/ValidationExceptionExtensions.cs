using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace Mafmax.InvestorService.Api.Extensions;

/// <summary>
/// Extensions for <see cref="ValidationException"/>
/// </summary>
public static class ValidationExceptionExtensions
{
    /// <summary>
    /// Gets the dictionary of errors
    /// </summary>
    public static IReadOnlyDictionary<string, string[]> ErrorsDictionary(this ValidationException exception) =>
        exception
            .Errors.GroupBy(x => x.PropertyName,
                x => x.ErrorMessage,
                (propName, error) => new
                {
                    Key = propName,
                    Values = error.Distinct().ToArray()
                }).ToDictionary(x => x.Key, x => x.Values);
}
