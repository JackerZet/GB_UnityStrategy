using System;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Utils.Extensions;

namespace Core
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        private class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
        {
            private readonly UnitMovementStop _unitMovementStop;

            public StopAwaiter(UnitMovementStop unitMovementStop)
            {
                _unitMovementStop = unitMovementStop;
                _unitMovementStop.OnStop += OnStop;
            }

            private void OnStop()
            {
                _unitMovementStop.OnStop -= OnStop;
                Complete(new AsyncExtensions.Void());
            }
        }

        [SerializeField] 
        private NavMeshAgent _agent;
        
        public event Action OnStop;

        private void Update()
        {
            if (_agent.enabled && !_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                    {
                        OnStop?.Invoke();
                    }
                }
            }
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);
    }
}