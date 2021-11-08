using System;

namespace Mafmax.InvestorService.Services.Exceptions
{

    /// <summary>
    /// Suitable handler not found
    /// </summary>
    public class CommandsHandlerNotFoundException : Exception
    {
        /// <inheritdoc />
        public CommandsHandlerNotFoundException(Type commandType, Type resultType) : 
            base($"Command handler with command type \"{commandType}\" and result type \"{resultType}\" not found")
        {
            
        }
    }
}