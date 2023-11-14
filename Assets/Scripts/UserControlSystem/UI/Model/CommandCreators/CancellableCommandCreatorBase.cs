using System;
using System.Threading;
using Abstractions.Commands;
using Utils;
using Utils.AssetsInjector;
using Utils.Extensions;
using Zenject;

namespace UserControlSystem.UI.Model.CommandCreators
{
    public abstract class CancellableCommandCreatorBase<TCommand, TArgument> : CommandCreatorBase<TCommand> where TCommand : ICommand
    {
        [Inject] 
        private AssetsContext _context;
        [Inject] 
        private IAwaitable<TArgument> _awaitableArgument;

        private CancellationTokenSource _ctSource;

        protected sealed override async void ClassSpecificCommandCreation(Action<TCommand> creationCallback)
        {
            _ctSource = new CancellationTokenSource();
            try
            {
                var argument = await _awaitableArgument.WithCancellation(_ctSource.Token);
                creationCallback?.Invoke(_context.Inject(CreateCommand(argument)));
            }
            catch
            {
                // ignored
            }
        }

        protected abstract TCommand CreateCommand(TArgument argument);

        public override void ProcessCancel()
        {
            base.ProcessCancel();

            if (_ctSource != null)
            {
                _ctSource.Cancel();
                _ctSource.Dispose();
                _ctSource = null;
            }
        }
    }
}