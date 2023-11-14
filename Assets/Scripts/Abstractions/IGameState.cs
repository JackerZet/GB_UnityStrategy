using System;

namespace Abstractions
{
    public interface IGameState
    {
        IObservable<int> State { get; }
    }
}