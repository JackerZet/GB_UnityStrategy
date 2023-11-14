using System.Threading;
using System.Threading.Tasks;
using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using UnityEngine;
using UnityEngine.AI;
using Utils.Extensions;

namespace Core.CommandExecutors
{
    public class CommandExecutorPatrol : CommandExecutorBase<IPatrolCommand>
    {
        private const int WAYPONTS_COUNT = 4;
        
        [SerializeField] 
        private float radius = 1;
        
        [SerializeField] 
        private NavMeshAgent _navMeshAgent;
        [SerializeField] 
        private NavMeshObstacle _obstacle;
        [SerializeField]
        private UnitMovementStop _stop;
        [SerializeField]
        private Animator _animator;
        
        [SerializeField] 
        private CommandExecutorStop _stopCommandExecutor;
        
        private Vector3[] _waypoints = new Vector3[WAYPONTS_COUNT];
        private int _currentWaypointIndex;
        private bool _isPatrolling = false;

        public override async Task ExecuteSpecificCommand(IPatrolCommand command)
        {
            GenerateWaypoints(command.CenterPoint);
            _animator.SetBool("IsWalking", true);
            _isPatrolling = true;
            _obstacle.enabled = false;
            _navMeshAgent.enabled = true;
            
            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
            
            while (_isPatrolling)
            {
                _navMeshAgent.SetDestination(_waypoints[_currentWaypointIndex]);
                
                try
                {
                    await _stop.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
                }
                catch
                {
                    _navMeshAgent.isStopped = true;
                    _navMeshAgent.ResetPath();
                    _isPatrolling = false;
                    _navMeshAgent.enabled = false;
                    _obstacle.enabled = true;
                }
                
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            }
            
            _stopCommandExecutor.CancellationTokenSource.Dispose();
            _stopCommandExecutor.CancellationTokenSource = null;

            _currentWaypointIndex = 0;
            _animator.SetBool("IsWalking", false);
        }


        private void GenerateWaypoints(Vector3 centerPoint)
        {
            var vectorR = new Vector3(1, 0, 1);
            var vectorL = new Vector3(1, 0, -1);

            for (var i = 0; i < 4; i++)
            {
                var radius1 = radius * (i < WAYPONTS_COUNT / 2 ? 1 : -1);

                _waypoints[i] = centerPoint + (i % 2 == 0 ? vectorR : vectorL) * radius1;
            }
        }
    }
}