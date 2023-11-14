using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Core.Items
{
    public class FactionsManager : IDisposable
    {
        private static readonly Lazy<FactionsManager> _instance = 
            new Lazy<FactionsManager>(() => new FactionsManager(), LazyThreadSafetyMode.ExecutionAndPublication);
        
        private Dictionary<int, List<int>> _members = new Dictionary<int, List<int>>();
        
        public static FactionsManager Instance => _instance.Value;

        public int FactionsCount => _members.Count;
        public int LastFaction => _members.Keys.First();

        public void Register(int factionId, int memberId)
        {
            if (!_members.ContainsKey(factionId))
            {
                _members.Add(factionId, new List<int>());
            }
            if (!_members[factionId].Contains(memberId))
            {
                _members[factionId].Add(memberId);
            }
        }

        public void Unregister(int factionId, int memberId)
        {
            if (_members[factionId].Contains(memberId))
            {
                _members[factionId].Remove(memberId);
            }
            if (_members[factionId].Count == 0)
            {
                _members.Remove(factionId);
            }

        }

        public void Dispose()
        {
            _members = null;
        }
    }
}