using System.Threading.Tasks;
using Mafmax.InvestorService.Model.Context;
using Mafmax.InvestorService.Services.MockData.Context;
using MediatR;
using static Mafmax.InvestorService.Services.MockData.Context.MockProvider;

namespace Mafmax.InvestorService.Services.Tests.Queries.Base;

public abstract class InvestorServiceQueriesHandlerTestsBase<THandler>
{

    // ReSharper disable once UnusedMember.Global
    protected abstract THandler GetHandler();

    private readonly IMediator _mediator;

    protected async Task<TResult> AskAsync<TResult>(IRequest<TResult> query) =>
        await _mediator.Send(query);

    protected InvestorDbContext GetContext() => MockProvider.GetContext("Readonly");

    protected InvestorServiceQueriesHandlerTestsBase()
    {
        var testingAssembly = typeof(THandler).Assembly;
        _mediator = GetMediator(testingAssembly);
    }
        
}