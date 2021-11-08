using System;

namespace Mafmax.InvestorService.Services.Exceptions
{

    /// <summary>
    /// Suitable handler not found
    /// </summary>
    public class QueriesHandlerNotFoundException : Exception
    {
        /// <inheritdoc />
        public QueriesHandlerNotFoundException(Type queryType, Type resultType)
        : base($"Query handler with query type \"{queryType}\" and result type \"{resultType}\" not found")
        {

        }
    }
}
