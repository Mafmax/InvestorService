using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Model.Entities.Users;
using Mafmax.InvestorService.Services.Services.Commands.Interfaces;
using Mafmax.InvestorService.Services.Services.Commands.Login;
using Microsoft.EntityFrameworkCore;

namespace Mafmax.InvestorService.Services.Services.Commands.Handlers;

/// <summary>
/// Handle commands associated with login
/// </summary>
public class LoginCommandsHandler : ServiceBase<InvestorDbContext>,
    ICommandHandler<RegisterInvestorCommand, int>
{
    /// <inheritdoc />
    public LoginCommandsHandler(InvestorDbContext db, IMapper mapper) : base(db, mapper) { }

    /// <summary>
    /// Registers investor
    /// </summary>
    /// <returns>Investor id</returns>
    public async Task<int> ExecuteAsync(RegisterInvestorCommand command)
    {
        var investor = Mapper.Map<InvestorEntity>(command);

        await Db.Investors.AddAsync(investor);

        await Db.SaveChangesAsync();

        return investor.Id;
    }
}