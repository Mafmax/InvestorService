using System.Threading.Tasks;

namespace Mafmax.InvestorService.Services.Services.Commands.Interfaces
{

    /// <summary>
    /// Defines methods to execute commands
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Executes command
        /// </summary>
        /// <typeparam name="TResult">Command type</typeparam>
        Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command);
    }
}