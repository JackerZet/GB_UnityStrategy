using Abstractions.Items;
using UnityEngine;

namespace Abstractions
{
    public interface ISelectable : IIconHolder
    {
        float Health { get; }
        float MaxHealth { get; }
        void Select();
        void UnSelect();
    }
}