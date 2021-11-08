using System;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using static Mafmax.InvestorService.Services.MockData.Context.MockProvider;

namespace Mafmax.InvestorService.Services.Tests.Commands.Base
{
    public abstract class InvestorServiceCommandsHandlerTestsBase<THandler>
    {
        protected THandler Handler => GetHandler();
        protected abstract THandler GetHandler(Guid token);
        protected THandler GetHandler() => GetHandler(Guid.NewGuid());

        protected Guid GetDbToken()=>Guid.NewGuid();

        protected IMapper Mapper;

        protected InvestorDbContext GetDb(Guid token) => GetContext(token);

        protected InvestorServiceCommandsHandlerTestsBase()
        {
            Mapper = GetMapper(typeof(THandler).Assembly);
        }

    }
}
