using System;
using Abstractions.Items;
using UnityEngine;
using Zenject;

namespace Core.Items
{
    [RequireComponent(typeof(IFactionMember))]
    public class FactionMemberParallelInfoUpdater : MonoBehaviour
    {
        private IFactionMember _factionMember;

        private void Start()
        {
            _factionMember = GetComponent<IFactionMember>();
        }

        private void Update()
        {
            AutoAttackEvaluator.FactionMembersInfo.AddOrUpdate(
                gameObject
                , new AutoAttackEvaluator.FactionMemberParallelInfo(transform.position, _factionMember.FactionId)
                , (go, value) =>
                {
                    value.Position = transform.position;
                    value.Faction = _factionMember.FactionId;
                    return value;
                });
        }

        private void OnDestroy()
        {
            AutoAttackEvaluator.FactionMembersInfo.TryRemove(gameObject, out _);
        }
    }
}