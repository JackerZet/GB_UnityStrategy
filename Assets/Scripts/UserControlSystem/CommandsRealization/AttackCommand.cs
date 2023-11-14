using Abstractions.Commands.CommandInterfaces;
using Abstractions.Items;

namespace UserControlSystem.CommandsRealization
{
    public class AttackCommand : IAttackCommand
    {
        public IDamageable Target { get; }

        public AttackCommand(IDamageable target)
        {
            Target = target;
        }
    }
}