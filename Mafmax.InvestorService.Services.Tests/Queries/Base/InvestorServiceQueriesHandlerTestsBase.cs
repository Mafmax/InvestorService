namespace Mafmax.InvestorService.Services.Tests.Queries.Base;

public abstract class InvestorServiceQueriesHandlerTestsBase<THandler>
{
    protected THandler Handler=>GetHandler();

    protected abstract THandler GetHandler();
}