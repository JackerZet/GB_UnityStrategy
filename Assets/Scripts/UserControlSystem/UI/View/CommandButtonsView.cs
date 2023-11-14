using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UserControlSystem.UI.View
{
    public class CommandButtonsView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _attackButton;
        [SerializeField] 
        private GameObject _moveButton;
        [SerializeField] 
        private GameObject _patrolButton;
        [SerializeField] 
        private GameObject _stopButton;
        [SerializeField] 
        private GameObject _produceUnitButton;

        private Dictionary<Type, GameObject> _buttonsByExecutorType;
        
        public Action<ICommandExecutor, ICommandsQueue> OnClick;
        
        private void Start()
        {
            _buttonsByExecutorType = new Dictionary<Type, GameObject>
            {
                {typeof(CommandExecutorBase<IAttackCommand>), _attackButton},
                {typeof(CommandExecutorBase<IMoveCommand>), _moveButton},
                {typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton},
                {typeof(CommandExecutorBase<IStopCommand>), _stopButton},
                {typeof(CommandExecutorBase<IProduceUnitCommand>), _produceUnitButton}
            };
            Clear();
        }
        
        public void BlockInteractions(ICommandExecutor ce)
        {
            UnblockAllInteractions();
            GetButtonGameObjectByType(ce.GetType())
                .GetComponent<Selectable>().interactable = false;
        }

        public void UnblockAllInteractions() => SetInteractable(true);

        private void SetInteractable(bool value)
        {
            _attackButton.GetComponent<Selectable>().interactable = value;
            _moveButton.GetComponent<Selectable>().interactable = value;
            _patrolButton.GetComponent<Selectable>().interactable = value;
            _stopButton.GetComponent<Selectable>().interactable = value;
            _produceUnitButton.GetComponent<Selectable>().interactable = value;
        }

        public void MakeLayout(IEnumerable<ICommandExecutor> commandExecutors, ICommandsQueue commandsQueue)
        {
            foreach (var currentExecutor in commandExecutors)
            {
                var buttonGameObject = _buttonsByExecutorType
                    .First(type => type
                        .Key
                        .IsInstanceOfType(currentExecutor))
                    .Value;
                buttonGameObject.SetActive(true);
                var button = buttonGameObject.GetComponent<Button>();
                button.OnClickAsObservable().TakeUntilDisable(buttonGameObject)
                    .Subscribe(_ => OnClick?.Invoke(currentExecutor, commandsQueue));
            }
        }
        
        private GameObject GetButtonGameObjectByType(Type executorInstanceType)
        {
            return _buttonsByExecutorType
                .First(type => type.Key.IsAssignableFrom(executorInstanceType))
                .Value;
        }

        public void Clear()
        {
            foreach (var kvp in _buttonsByExecutorType)
            {
                kvp.Value.SetActive(false);
            }
        }
    }
}