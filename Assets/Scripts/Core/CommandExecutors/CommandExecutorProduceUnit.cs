using System.Threading.Tasks;
using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using Abstractions.Items.Production;
using Core.CommandsRealization;
using Core.Items;
using Core.UnitTasks;
using UniRx;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class CommandExecutorProduceUnit : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        private const int MIN_INCLUSIVE = -10;
        private const int MAX_INCLUSIVE = 10;
        private const int ZERO = 0;
        
        [SerializeField] 
        private Transform _unitsParent;
        [SerializeField] 
        private Transform _pickupPoint;
        [SerializeField] 
        private int _maximumUnitsInQueue = 6;
        
        [SerializeField] 
        private GameObject _unitPrefab;

        [SerializeField] 
        private string _unitName;

        [SerializeField] 
        private Sprite _unitIcon;

        [SerializeField] 
        private float _productionTime = 5f;

        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

        private void InstantiateUnit(GameObject unitPrefab)
        {
            var xRandom = Random.Range(MIN_INCLUSIVE, MAX_INCLUSIVE);
            var zRandom = Random.Range(MIN_INCLUSIVE, MAX_INCLUSIVE);
            var unit = Instantiate(unitPrefab, new Vector3(xRandom, ZERO, zRandom), Quaternion.identity,
                _unitsParent);

            unit.GetComponent<CommandExecutorMove>().ExecuteCommand(new MoveCommand(_pickupPoint.position));
            unit.GetComponent<FactionMember>().SetFaction(GetComponent<FactionMember>().FactionId);
        }
        
        private void Update()
        {
            if (_queue.Count == 0)
            {
                return;
            }

            var innerTask = (UnitProductionTask)_queue[0];
            innerTask.TimeLeft -= Time.deltaTime;
            
            if (innerTask.TimeLeft <= 0)
            {
                RemoveTaskAtIndex(0);
                InstantiateUnit(innerTask.UnitPrefab);
            }
        }
        
        private void RemoveTaskAtIndex(int index)
        {
            for (var i = index; i < _queue.Count - 1; i++)
            {
                _queue[i] = _queue[i + 1];
            }
            
            _queue.RemoveAt(_queue.Count - 1);
        }

        public void Cancel(int index) => RemoveTaskAtIndex(index);
        
        public override Task ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            if (_queue.Count >= _maximumUnitsInQueue)
                return Task.CompletedTask;
            
            _queue.Add(new UnitProductionTask(_productionTime, _unitIcon, _unitPrefab, _unitName));
            return Task.CompletedTask;
        }
    }
}