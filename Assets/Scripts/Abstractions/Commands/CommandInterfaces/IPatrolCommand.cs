using UnityEngine;

namespace Abstractions.Commands.CommandInterfaces
{
    public interface IPatrolCommand : ICommand
    {
        Vector3 CenterPoint { get; }
    }
}