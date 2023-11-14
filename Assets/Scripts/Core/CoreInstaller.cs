using Abstractions;
using Zenject;
using Core.Timers;
using UnityEngine;

namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField]
        private GameState _gameState;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
            Container.Bind<IGameState>().FromInstance(_gameState);
        }
    }
}