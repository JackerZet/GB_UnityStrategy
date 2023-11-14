using System;
using Abstractions.Time;
using UniRx;
using Zenject;
using UnityEngine;

namespace Core.Timers
{
    public class TimeModel : ITimeModel, ITickable
    {
        private ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();
        
        public IObservable<int> GameTime => _gameTime.Select(f => (int) f);

        public void Tick()
        {
            _gameTime.Value += Time.deltaTime;
        }
    }
}