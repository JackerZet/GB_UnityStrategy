using System;
using Abstractions.Commands;
using Abstractions.Items;
using UnityEngine;
using Zenject;

namespace Core.Items
{
    [RequireComponent(typeof(IAutomaticAttacker), typeof(ICommandsQueue))]
    public class AttackerParallelInfoUpdater : MonoBehaviour
    {
        private IAutomaticAttacker _automaticAttacker;
        private ICommandsQueue _queue;

        private void Start()
        {
            _automaticAttacker = GetComponent<IAutomaticAttacker>();
            _queue = GetComponent<ICommandsQueue>();
        }

        private void Update()
        {
            AutoAttackEvaluator.AttackersInfo.AddOrUpdate(
                gameObject
                , new AutoAttackEvaluator.AttackerParallelInfo(_automaticAttacker.VisionRadius, _queue.CurrentCommand)
                , (go, value) =>
                {
                    value.VisionRadius = _automaticAttacker.VisionRadius;
                    value.CurrentCommand = _queue.CurrentCommand;
                    return value;
                });
        }

        private void OnDestroy()
        {
            AutoAttackEvaluator.AttackersInfo.TryRemove(gameObject, out _);
        }
    }
}