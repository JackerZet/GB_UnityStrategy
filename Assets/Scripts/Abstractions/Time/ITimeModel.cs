using System;

namespace Abstractions.Time
{
    public interface ITimeModel
    {
        IObservable<int> GameTime { get; }
    }
}