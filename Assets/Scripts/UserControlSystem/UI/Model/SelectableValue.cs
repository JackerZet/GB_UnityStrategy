using Abstractions;
using UnityEngine;
using UserControlSystem.UI.Model.ChangeableValueModels;

namespace UserControlSystem.UI.Model
{
    [CreateAssetMenu(
        fileName = nameof(SelectableValue),
        menuName = "Strategy Game/" + nameof(SelectableValue))]
    public sealed class SelectableValue : StatefulChangeableValueModel<ISelectable>
    {
        public override void ChangeValue(ISelectable value)
        {
            CurrentValue?.UnSelect();
            base.ChangeValue(value);
            CurrentValue?.Select();
        }
    }
}
