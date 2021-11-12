using System;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using static Mafmax.InvestorService.Services.MockData.Context.MockProvider;

namespace Mafmax.InvestorService.Services.Tests.Commands.Base;

public abstract class InvestorServiceCommandsHandlerTestsBase<THandler>
{
    protected THandler Handler => GetHandler(Guid.NewGuid());
    protected abstract THandler GetHandler(Guid token);

    protected Guid GetDbToken() => Guid.NewGuid();

    protected readonly IMapper Mapper;

    protected InvestorDbContext GetDb(Guid token) => GetContext(token);

    protected InvestorServiceCommandsHandlerTestsBase()
    {
        Mapper = GetMapper(typeof(THandler).Assembly);
    }

}