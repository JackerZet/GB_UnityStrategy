using System;
using System.Threading.Tasks;

namespace Abstractions.Commands
{
    public interface ICommandExecutor
    {
        Task ExecuteCommand(object command);

        Type GetCommandType();
    }
}