using System;
using Abstractions.Commands.CommandInterfaces;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using Utils.AssetsInjector;
using Zenject;

namespace UserControlSystem.UI.Model.CommandCreators
{
    public class MoveCommandCommandCreator : CancellableCommandCreatorBase<IMoveCommand, Vector3>
    {
        protected override IMoveCommand CreateCommand(Vector3 point) => new MoveCommand(point);
    }
}