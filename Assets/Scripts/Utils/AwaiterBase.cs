using System;

namespace Utils
{
    public abstract class AwaiterBase<T> : IAwaiter<T>
    {
        private T _result;
        private Action _continuation;
        private bool _isCompleted;

        public bool IsCompleted => _isCompleted;

        protected void Complete(T result)
        {
            _result = result;
            _isCompleted = true;
            _continuation?.Invoke();
        }
        
        public void OnCompleted(Action continuation)
        {
            if (_isCompleted)
            {
                continuation?.Invoke();
            }
            else
            {
                _continuation = continuation;
            }
        }

        public T GetResult() => _result;
    }
}