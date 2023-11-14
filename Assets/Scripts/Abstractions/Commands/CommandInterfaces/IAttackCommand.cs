using Abstractions.Items;

namespace Abstractions.Commands.CommandInterfaces
{
    public interface IAttackCommand : ICommand
    {
        IDamageable Target { get; }
    }
}