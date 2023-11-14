using System;
using UnityEngine;
using Utils;

namespace UserControlSystem.UI.Model.ChangeableValueModels
{
    public abstract class ChangeableValueModel<T> : ScriptableObject, IAwaitable<T>
    {
        private class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
        {
            private readonly ChangeableValueModel<TAwaited> _changeableValueModel;
            
            public NewValueNotifier(ChangeableValueModel<TAwaited> changeableValueModel)
            {
                _changeableValueModel = changeableValueModel;
                _changeableValueModel.OnChanged += OnNewValue;
            }

            private void OnNewValue(TAwaited obj)
            {
                _changeableValueModel.OnChanged -= OnNewValue;
                Complete(obj);
            }
        }
        
        public T CurrentValue { get; private set; }
        public event Action<T> OnChanged;

        public virtual void ChangeValue(T value)
        {
            CurrentValue = value;
            OnChanged?.Invoke(value);
        }

        public IAwaiter<T> GetAwaiter()
        {
            return new NewValueNotifier<T>(this);
        }
    }
}