using UnityEngine;
using UserControlSystem.UI.Model.ChangeableValueModels;

namespace UserControlSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(Vector3Value), menuName = "Strategy Game/" + nameof(Vector3Value))]
    public class Vector3Value : StatelessChangeableValueModel<Vector3>
    {
    }
}