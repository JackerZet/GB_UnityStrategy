using Abstractions.Commands.CommandInterfaces;
using Abstractions.Items;

namespace Core.CommandsRealization
{
    public class AutoAttackCommand : IAttackCommand
    {
        public IDamageable Target { get; }

        public AutoAttackCommand(IDamageable target)
        {
            Target = target;
        }

    }
}