using UnityEngine;

namespace Abstractions.Commands.CommandInterfaces
{
    public interface IMoveCommand : ICommand
    {
        Vector3 Target { get; }
    }
}