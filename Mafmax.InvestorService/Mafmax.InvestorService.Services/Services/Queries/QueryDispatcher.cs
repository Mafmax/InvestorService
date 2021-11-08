using System;
using System.Threading.Tasks;
using Mafmax.InvestorService.Services.Exceptions;
using Mafmax.InvestorService.Services.Services.Queries.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Mafmax.InvestorService.Services.Services.Queries
{

    /// <summary>
    /// Class to handle queries
    /// </summary>
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _services;

        /// <summary>
        /// Creates dispatcher
        /// </summary>
        /// <param name="services"></param>
        public QueryDispatcher(IServiceProvider services)
        {
            _services = services;
        }
        
        /// <summary>
        /// Handle query
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns>Result of query</returns>
        public async Task<TResult> AskAsync<TResult>(IQuery<TResult> query)
        {
            if (query is null) throw new ArgumentNullException(nameof(query));

            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            var handler = _services.GetRequiredService(handlerType);

            if (handler is null) throw new QueriesHandlerNotFoundException(query.GetType(), typeof(TResult));

            var askAsyncTask = (Task<TResult>?)handlerType
                .GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.AskAsync),new []{query.GetType()})
                ?.Invoke(handler, new object[] { query });

            if (askAsyncTask is null) throw new Exception("Could not load query handler method");

            return await askAsyncTask;
        }


    }
}