using Abstractions.Commands.CommandInterfaces;
using Abstractions.Items;
using UserControlSystem.CommandsRealization;

namespace UserControlSystem.UI.Model.CommandCreators
{
    public class AttackCommandCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IDamageable>
    {
        protected override IAttackCommand CreateCommand(IDamageable argument) => new AttackCommand(argument);
    }
}