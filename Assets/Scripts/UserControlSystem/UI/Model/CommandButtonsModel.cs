using System;
using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using UserControlSystem.UI.Model.CommandCreators;
using Zenject;

namespace UserControlSystem.UI.Model
{
    public class CommandButtonsModel
    {
        [Inject] 
        private CommandCreatorBase<IProduceUnitCommand> _unitProducer;
        [Inject] 
        private CommandCreatorBase<IAttackCommand> _attacker;
        [Inject] 
        private CommandCreatorBase<IStopCommand> _stopper;
        [Inject] 
        private CommandCreatorBase<IMoveCommand> _mover;
        [Inject] 
        private CommandCreatorBase<IPatrolCommand> _patroller;

        private bool _commandIsPending;
        
        public event Action<ICommandExecutor> OnCommandAccepted;
        public event Action OnCommandSent;
        public event Action OnCommandCancel;

        public void OnCommandButtonClicked(ICommandExecutor commandExecutor, ICommandsQueue commandsQueue)
        {
            if (_commandIsPending)
            {
                processOnCancel();
            }
            _commandIsPending = true;
            OnCommandAccepted?.Invoke(commandExecutor);

            _unitProducer.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command, commandsQueue));
            _attacker.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command, commandsQueue));
            _stopper.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command, commandsQueue));
            _mover.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command, commandsQueue));
            _patroller.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command, commandsQueue));
        }

        public void ExecuteCommandWrapper(ICommandExecutor commandExecutor, object command, ICommandsQueue commandsQueue)
        {
            if (commandsQueue != null && command is ICommand rightCommand)
            {
                commandsQueue.EnqueueCommand(rightCommand);
            }
            else
            {
                commandExecutor.ExecuteCommand(command);
            }

            _commandIsPending = false;
            OnCommandSent?.Invoke();
        }

        public void OnSelectionChanged()
        {
            _commandIsPending = false;
            processOnCancel();
        }

        private void processOnCancel()
        {
            _unitProducer.ProcessCancel();
            _attacker.ProcessCancel();
            _stopper.ProcessCancel();
            _mover.ProcessCancel();
            _patroller.ProcessCancel();

            OnCommandCancel?.Invoke();
        }
    }
}