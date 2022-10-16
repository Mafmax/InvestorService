using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Model.Entities;
using Mafmax.InvestorService.Model.Entities.Users;
using Mafmax.InvestorService.Model.Extensions;
using Mafmax.InvestorService.Services.Commands.Portfolios;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Mafmax.InvestorService.Services.Exceptions.Helpers.ThrowsHelper;

namespace Mafmax.InvestorService.Services.Commands.Handlers;

/// <summary>
/// Handle commands associated with portfolios
/// </summary>
public class PortfolioCommandsHandler : ServiceBase<InvestorDbContext>,
    IRequestHandler<CreatePortfolioCommand, PortfolioDetailedInfoDto>,
    IRequestHandler<DeletePortfolioCommand>,
    IRequestHandler<UpdatePortfolioCommand, PortfolioDetailedInfoDto>
{
    /// <inheritdoc />
    public PortfolioCommandsHandler(InvestorDbContext db, IMapper mapper) : base(db, mapper) { }

    /// <summary>
    /// Creates portfolio
    /// </summary>
    /// <returns>Created portfolio</returns>
    ///<exception cref="EntityNotFoundException"/>
    ///<exception cref="InvalidOperationException"/>
    public async Task<PortfolioDetailedInfoDto> Handle(CreatePortfolioCommand command, CancellationToken token)
    {
        var investor = await Db.Investors
            .Include(x => x.Portfolios)
            .ByIdAsync(command.InvestorId, token);

        Validate(investor);

        var newPortfolio = Mapper.Map<InvestmentPortfolioEntity>(command);

        investor.Portfolios.Add(newPortfolio);

        await Db.SaveChangesAsync(token);

        return Mapper.Map<PortfolioDetailedInfoDto>(newPortfolio);

        void Validate([NotNull] InvestorEntity? i)
        {
            if (string.IsNullOrEmpty(command.Name))
                throw new InvalidOperationException("Name expected");

            if (string.IsNullOrEmpty(command.TargetDescription))
                throw new InvalidOperationException("Target description expected");

            if (i is null)
                ThrowEntityNotFound<InvestorEntity>(command.InvestorId);

            if (i.Portfolios.Count >= command.PortfoliosCountLimit)
                throw new InvalidOperationException($"Limit of portfolios ({command.PortfoliosCountLimit}) has been reached");
        }
    }

    /// <summary>
    /// Removes portfolio
    /// </summary>
    /// <exception cref="EntityNotFoundException"/>
    /// <exception cref="InvalidOperationException"/>
    public async Task<Unit> Handle(DeletePortfolioCommand command, CancellationToken token)
    {
        var investor = await Db.Investors
            .Include(x => x.Portfolios)
            .ByIdAsync(command.InvestorId, token);

        var portfolio = investor?.Portfolios
            .ById(command.PortfolioId);

        Validate(investor, portfolio);

        Db.InvestmentPortfolios.Remove(portfolio);

        await Db.SaveChangesAsync(token);

        return Unit.Value;

        void Validate([NotNull] InvestorEntity? i,
            [NotNull] InvestmentPortfolioEntity? ip)
        {
            if (i is null) ThrowEntityNotFound<InvestorEntity>(command.InvestorId);

            if (ip is null)
                ThrowIncludedEntityNotFound<InvestorEntity, InvestmentPortfolioEntity>(command.InvestorId, command.PortfolioId);

            if (i.Portfolios.Count.Equals(1))
                throw new InvalidOperationException("Cannot delete the last portfolio");
        }
    }

    /// <summary>
    /// Changes portfolio parameters, which not null
    /// </summary>
    /// <exception cref="EntityNotFoundException"/>
    public async Task<PortfolioDetailedInfoDto> Handle(UpdatePortfolioCommand command, CancellationToken token)
    {
        var investor = await Db.Investors
            .Include(x => x.Portfolios)
            .ByIdAsync(command.InvestorId, token);

        var portfolio = await Db.InvestmentPortfolios.ByIdAsync(command.PortfolioId, token);

        Validate(investor, portfolio);

        if (command.NewName is not null)
            portfolio.UpdateName(command.NewName);
        
        if (command.NewTargetDescription is not null)
            portfolio.UpdateTargetDescription(command.NewTargetDescription);

        await Db.SaveChangesAsync(token);

        return Mapper.Map<PortfolioDetailedInfoDto>(portfolio);

        void Validate([NotNull] InvestorEntity? i,
            [NotNull] InvestmentPortfolioEntity? ip)
        {
            if (i is null)
                ThrowEntityNotFound<InvestorEntity>(command.InvestorId);

            if (ip is null || !i.Portfolios.Contains(ip))
                ThrowIncludedEntityNotFound<InvestorEntity, InvestmentPortfolioEntity>(command.InvestorId, command.PortfolioId);
        }
    }
}