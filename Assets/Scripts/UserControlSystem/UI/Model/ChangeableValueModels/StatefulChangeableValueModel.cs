using System;
using UniRx;

namespace UserControlSystem.UI.Model.ChangeableValueModels
{
    public class StatefulChangeableValueModel<T> : ChangeableValueModel<T>, IObservable<T>
    {
        private ReactiveProperty<T> _stream = new ReactiveProperty<T>();

        public override void ChangeValue(T value)
        {
            base.ChangeValue(value);
            _stream.Value = value;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _stream.Subscribe(observer);
        }
    }
}