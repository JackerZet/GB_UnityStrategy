using Abstractions.Items;
using UnityEngine;
using UserControlSystem.UI.Model.ChangeableValueModels;

namespace UserControlSystem.UI.Model
{
    [CreateAssetMenu(
        fileName = nameof(DamageableValue), 
        menuName = "Strategy Game/" + nameof(DamageableValue))]
    public sealed class DamageableValue : StatelessChangeableValueModel<IDamageable>
    {
    }
}
