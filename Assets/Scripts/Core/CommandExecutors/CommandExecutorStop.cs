using System.Threading;
using System.Threading.Tasks;
using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class CommandExecutorStop : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }

        public override Task ExecuteSpecificCommand(IStopCommand command)
        {
            Debug.Log("Stop");
            CancellationTokenSource?.Cancel();
            return Task.CompletedTask;
        }
    }
}