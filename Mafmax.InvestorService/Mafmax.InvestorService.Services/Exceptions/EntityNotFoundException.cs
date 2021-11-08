using System;

namespace Mafmax.InvestorService.Services.Exceptions
{

    /// <summary>
    /// Entity could not find
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        /// <inheritdoc />
        public EntityNotFoundException(string message) : base(message) { }
    }
}
