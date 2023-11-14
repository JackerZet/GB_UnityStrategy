using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.Commands
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor where T : ICommand
    {
        public Task ExecuteCommand(object command) => ExecuteSpecificCommand((T)command);

        public abstract Task ExecuteSpecificCommand(T command);

        public Type GetCommandType()
        {
            return typeof(T);
        }
    }
}