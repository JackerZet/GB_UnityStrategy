using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using UniRx;
using UnityEngine;

namespace Core
{
    public class UnitCommandsQueue : MonoBehaviour, ICommandsQueue
    {
        private ReactiveCollection<ICommand> _queue = new ReactiveCollection<ICommand>();
        private Dictionary<Type, ICommandExecutor> _commandExecutors;

        public ICommand CurrentCommand => _queue.Count > 0 ? _queue[0] : default;
        
        private void Start()
        {
            _commandExecutors = GetComponents<ICommandExecutor>().ToDictionary(commandExecutor => commandExecutor.GetCommandType());

            _queue.ObserveAdd().Subscribe(addEvent => OnAddCommand(addEvent.Value, addEvent.Index)).AddTo(this);
        }

        private void OnAddCommand(ICommand command, int index)
        {
            if (index == 0)
            {
                ExecuteCommand(command);
            }
        }

        private async void ExecuteCommand(ICommand command)
        {
            var commandInterface = command.GetType().GetInterfaces()
                .FirstOrDefault(type => type.GetTypeInfo().ImplementedInterfaces.Contains(typeof(ICommand)));
            
            if (_commandExecutors.TryGetValue(commandInterface!, out var commandExecutor))
            {
                await commandExecutor.ExecuteCommand(command);

                if (_queue.Count > 0)
                {
                    _queue.RemoveAt(0);
                }
                
                if (_queue.Count > 0)
                {
                    ExecuteCommand(_queue[0]);
                }
            }
        }

        public void EnqueueCommand(ICommand command)
        {
            if (command is IStopCommand)
            {
                _queue.Clear();
            }
            
            _queue.Add(command);
        }
    }
}