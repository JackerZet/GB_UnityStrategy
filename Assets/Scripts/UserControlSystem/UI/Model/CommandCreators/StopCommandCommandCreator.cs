using System;
using Abstractions.Commands.CommandInterfaces;
using UserControlSystem.CommandsRealization;
using Utils.AssetsInjector;
using Zenject;

namespace UserControlSystem.UI.Model.CommandCreators
{
    public class StopCommandCommandCreator : CommandCreatorBase<IStopCommand>
    {
        [Inject] 
        private AssetsContext _context;

        protected override void ClassSpecificCommandCreation(Action<IStopCommand> creationCallback)
        {
            creationCallback?.Invoke(new StopCommand());
        }
    }
}