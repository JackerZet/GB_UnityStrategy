using System;
using Abstractions.Items;
using UnityEngine;

namespace Core.Items
{
    public class FactionMember : MonoBehaviour, IFactionMember
    {
        [SerializeField]
        private int _factionId;

        public int FactionId => _factionId;
        
        public void SetFaction(int factionId)
        {
            _factionId = factionId;
            
            FactionsManager.Instance.Register(factionId, GetInstanceID());
        }

        private void Awake()
        {
            FactionsManager.Instance.Register(_factionId, GetInstanceID());
        }
        
        private void OnDestroy()
        {
            FactionsManager.Instance.Unregister(_factionId, GetInstanceID());
        }
    }
}