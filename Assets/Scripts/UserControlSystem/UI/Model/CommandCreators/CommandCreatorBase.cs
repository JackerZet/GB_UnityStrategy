﻿using System;
using Abstractions.Commands;

namespace UserControlSystem.UI.Model.CommandCreators
{
    public abstract class CommandCreatorBase<T> where T : ICommand
    {
        public ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback)
        {
            if (commandExecutor is CommandExecutorBase<T> classSpecificExecutor)
            {
                ClassSpecificCommandCreation(callback);
            }
            return commandExecutor;
        }

        protected abstract void ClassSpecificCommandCreation(Action<T> creationCallback);

        public virtual void ProcessCancel() { }
    }
}