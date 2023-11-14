using Abstractions.Items;
using Core.CommandsRealization;
using UniRx;
using UnityEngine;

namespace Core.Items
{
    public class AutoAttackAgent : MonoBehaviour
    {
        [SerializeField] 
        private UnitCommandsQueue _queue;

        private void Start()
        {
            AutoAttackEvaluator.AutoAttackCommands
                .ObserveOnMainThread()
                .Where(command => command.Attacker == gameObject)
                .Where(command => command.Attacker != null && command.Target != null)
                .Subscribe(command => AutoAttack(command.Target))
                .AddTo(this);
        }

        private void AutoAttack(GameObject target)
        {
            _queue.EnqueueCommand(new StopCommand());
            _queue.EnqueueCommand(new AutoAttackCommand(target.GetComponent<IDamageable>()));
        }
    }
}