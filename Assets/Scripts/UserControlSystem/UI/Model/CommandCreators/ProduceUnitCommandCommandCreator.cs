using System;
using Abstractions.Commands.CommandInterfaces;
using UserControlSystem.CommandsRealization;
using Utils.AssetsInjector;
using Zenject;

namespace UserControlSystem.UI.Model.CommandCreators
{
    public class ProduceUnitCommandCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        [Inject] 
        private AssetsContext _context;

        protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
        {
            var produceUnitCommand = _context.Inject(new ProduceUnitCommandHeir());
            creationCallback?.Invoke(produceUnitCommand);
        }
    }
}