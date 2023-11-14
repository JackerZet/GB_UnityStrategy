using Abstractions.Commands.CommandInterfaces;
using UnityEngine;

namespace UserControlSystem.CommandsRealization
{
    public class PatrolCommand : IPatrolCommand
    {
        public Vector3 CenterPoint { get; }

        public PatrolCommand(Vector3 centerPoint)
        {
            CenterPoint = centerPoint;
        }
    }
}