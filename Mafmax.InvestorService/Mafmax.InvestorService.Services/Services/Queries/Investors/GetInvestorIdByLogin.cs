
using FluentValidation;
using Mafmax.InvestorService.Services.Validation;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Queries.Investors;

/// <summary>
/// Query to get investor id by login
/// </summary>
public record GetInvestorIdByLogin(string Login) : IRequest<int>
{
    /// <inheritdoc />
    // ReSharper disable once UnusedType.Global
    public class Validator : AbstractValidator<GetInvestorIdByLogin>
    {
        /// <inheritdoc />
        public Validator()
        {
            RuleFor(x => x.Login)
                .SetValidator(new LoginValidator<GetInvestorIdByLogin>());
        }
    }
}