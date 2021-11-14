using System;
using System.Threading.Tasks;
using AutoMapper;
using Mafmax.InvestorService.Model.Context;
using MediatR;
using static Mafmax.InvestorService.Services.MockData.Context.MockProvider;

namespace Mafmax.InvestorService.Services.Tests.Commands.Base;

public abstract class InvestorServiceCommandsHandlerTestsBase<THandler>
{
    // ReSharper disable once UnusedMemberInSuper.Global
    protected abstract THandler GetHandler(Guid token);

    protected Guid GetDbToken() => Guid.NewGuid();

    protected readonly IMapper Mapper;

    protected InvestorDbContext GetDb(Guid token) => GetContext(token);

    private readonly IMediator _mediator;

    // ReSharper disable once UnusedMethodReturnValue.Global
    protected async Task<TResult> Execute<TResult>(IRequest<TResult> command) => 
        await _mediator.Send(command);

    protected InvestorServiceCommandsHandlerTestsBase()
    {
        var testingAssembly = typeof(THandler).Assembly;
        Mapper = GetMapper(testingAssembly);
        _mediator = GetMediator(testingAssembly);
    }

}