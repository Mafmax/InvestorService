using System.Threading.Tasks;

namespace Mafmax.InvestorService.Services.Services.Commands.Interfaces
{
    /// <summary>
    /// Defines methods to execute commands
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
    {
        /// <summary>
        /// Executes command
        /// </summary>
        Task<TResult> ExecuteAsync(TCommand command);
    }
}