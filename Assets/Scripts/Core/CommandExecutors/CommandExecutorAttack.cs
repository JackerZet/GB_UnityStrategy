using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using Abstractions.Items;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Utils.Extensions;
using Zenject;

namespace Core.CommandExecutors
{
    public partial class CommandExecutorAttack : CommandExecutorBase<IAttackCommand>
    {
        [SerializeField] 
        private Animator _animator;
        [SerializeField] 
        private CommandExecutorStop _stopCommandExecutor;

        [SerializeField] 
        private float _attackingDistance = 2f;
        [SerializeField] 
        private int _attackingPeriod = 1400;
        
        [SerializeField] 
        private NavMeshAgent _navMeshAgent;
        [SerializeField] 
        private NavMeshObstacle _obstacle;

        private IHealthHolder _ourHealth;

        private Vector3 _ourPosition;
        private Vector3 _targetPosition;
        private Quaternion _ourRotation;

        private readonly Subject<Vector3> _targetPositions = new Subject<Vector3>();
        private readonly Subject<Quaternion> _targetRotations = new Subject<Quaternion>();
        private readonly Subject<IDamageable> _attackTargets = new Subject<IDamageable>();

        private Transform _targetTransform;
        private AttackOperation _currentAttackOp;

        public void Awake()
        {
            _targetPositions
                .Select(value => new Vector3((float) Math.Round(value.x, 2), (float) Math.Round(value.y, 2),
                    (float) Math.Round(value.z, 2)))
                .Distinct()
                .ObserveOnMainThread()
                .Subscribe(StartMovingToPosition);

            _attackTargets
                .ObserveOnMainThread()
                .Subscribe(StartAttackingTargets);

            _targetRotations
                .ObserveOnMainThread()
                .Subscribe(SetAttackRotation);

            _ourHealth = GetComponent<IHealthHolder>();
        }

        private void SetAttackRotation(Quaternion targetRotation)
        {
            transform.rotation = targetRotation;
        }

        private void StartAttackingTargets(IDamageable target)
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
            _animator.SetTrigger("Attack");
            _animator.SetBool("IsWalking", false);
            target.ReceiveDamage(GetComponent<IDamageDealer>().Damage);
        }

        private void StartMovingToPosition(Vector3 position)
        {
            _obstacle.enabled = false;
            _navMeshAgent.enabled = true;
            _navMeshAgent.destination = position;
            _animator.SetBool("IsWalking", true);
        }

        private bool IsFriendlyTarget(Component commandTarget)
        {
            var targetFactionMember = commandTarget.GetComponent<IFactionMember>();
            var factionMember = GetComponent<IFactionMember>();

            return factionMember.FactionId == targetFactionMember.FactionId;
        }
        
        public override async Task ExecuteSpecificCommand(IAttackCommand command)
        {
            var commandTarget = command.Target as Component;

            if (IsFriendlyTarget(commandTarget))
                return;

            _targetTransform = commandTarget.transform;
            _currentAttackOp = new AttackOperation(this, command.Target);
            Update();
            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _currentAttackOp.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
            }
            catch
            {
                _currentAttackOp.Cancel();
            }

            _animator.SetBool("IsAttacking", false);
            _currentAttackOp = null;
            _targetTransform = null;
            _stopCommandExecutor.CancellationTokenSource = null;
            
            _navMeshAgent.enabled = false;
            _obstacle.enabled = true;
        }

        private void Update()
        {
            if (_currentAttackOp == null)
            {
                return;
            }

            lock (this)
            {
                _ourPosition = transform.position;
                _ourRotation = transform.rotation;
                if (_targetTransform != null)
                {
                    _targetPosition = _targetTransform.position;
                }
            }
        }

        public void AttackBegin()
        {
            //play sound
        }
        
        public void AttackEnd()
        {
            //play sound
        }
    }
}