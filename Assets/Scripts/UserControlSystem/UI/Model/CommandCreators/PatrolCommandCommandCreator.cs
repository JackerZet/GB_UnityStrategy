using System;
using Abstractions.Commands.CommandInterfaces;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using Utils.AssetsInjector;
using Zenject;

namespace UserControlSystem.UI.Model.CommandCreators
{
    public class PatrolCommandCommandCreator : CancellableCommandCreatorBase<IPatrolCommand, Vector3>
    {
        protected override IPatrolCommand CreateCommand(Vector3 point) => new PatrolCommand(point);
    }
}