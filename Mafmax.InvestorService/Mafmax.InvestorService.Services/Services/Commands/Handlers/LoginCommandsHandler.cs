using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Model.Entities.Users;
using Mafmax.InvestorService.Services.Services.Commands.Login;
using MediatR;

namespace Mafmax.InvestorService.Services.Services.Commands.Handlers;

/// <summary>
/// Handle commands associated with login
/// </summary>
public class LoginCommandsHandler : ServiceBase<InvestorDbContext>,
    IRequestHandler<RegisterInvestorCommand, int>
{
    /// <inheritdoc />
    public LoginCommandsHandler(InvestorDbContext db, IMapper mapper) : base(db, mapper) { }

    /// <summary>
    /// Registers investor
    /// </summary>
    /// <returns>Investor id</returns>
    public async Task<int> Handle(RegisterInvestorCommand command, CancellationToken token)
    {
        var investor = Mapper.Map<InvestorEntity>(command);

        await Db.Investors.AddAsync(investor, token);

        await Db.SaveChangesAsync(token);

        return investor.Id;
    }
}