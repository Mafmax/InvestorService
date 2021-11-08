using System.Threading.Tasks;

namespace Mafmax.InvestorService.Services.Services.Queries.Interfaces
{
    /// <summary>
    /// Defines methods to handle query
    /// </summary>
    public interface IQueryHandler<in TQuery,TResult> where TQuery : IQuery<TResult>
    {

        /// <summary>
        /// Handle query
        /// </summary>
        /// <returns>Query result</returns>
        Task<TResult> AskAsync(TQuery query);
    }
}
