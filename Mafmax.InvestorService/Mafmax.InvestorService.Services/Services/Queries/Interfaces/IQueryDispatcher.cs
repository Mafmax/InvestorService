using System.Threading.Tasks;

namespace Mafmax.InvestorService.Services.Services.Queries.Interfaces;

/// <summary>
/// Defines methods to handle queries
/// </summary>
public interface IQueryDispatcher
{
    /// <summary>
    /// Handle query
    /// </summary>
    /// <returns>Result of query</returns>
    Task<TResult> AskAsync<TResult>(IQuery<TResult> query);
}