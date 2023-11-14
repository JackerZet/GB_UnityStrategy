using System;
using System.Threading;
using Abstractions;
using Core.Items;
using UniRx;
using UnityEngine;

namespace Core
{
    public class GameState : MonoBehaviour, IGameState
    {
        private Subject<int> _state = new Subject<int>();

        public IObservable<int> State => _state;
        private void CheckState(object state)
        {
            switch (FactionsManager.Instance.FactionsCount)
            {
                case 0:
                    _state.OnNext(0);
                    break;
                case 1:
                    _state.OnNext(FactionsManager.Instance.LastFaction);
                    break;
            }
        }

        private void Update()
        {
            ThreadPool.QueueUserWorkItem(CheckState);
        }
    }
}