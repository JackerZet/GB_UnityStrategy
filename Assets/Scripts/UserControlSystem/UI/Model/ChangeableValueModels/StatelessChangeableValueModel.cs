using System;
using UniRx;

namespace UserControlSystem.UI.Model.ChangeableValueModels
{
    public class StatelessChangeableValueModel<T> : ChangeableValueModel<T>, IObservable<T>
    {
        private Subject<T> _stream = new Subject<T>();

        public override void ChangeValue(T value)
        {
            base.ChangeValue(value);
            _stream.OnNext(value);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _stream.Subscribe(observer);
        }
    }
}