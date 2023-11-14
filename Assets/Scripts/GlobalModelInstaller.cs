using System;
using Abstractions;
using Abstractions.Items;
using UnityEngine;
using UnityEngine.SceneManagement;
using UserControlSystem.UI.Model;
using Utils;
using Utils.AssetsInjector;
using Zenject;

[CreateAssetMenu(fileName = nameof(GlobalModelInstaller), menuName = "Installers/" + nameof(GlobalModelInstaller))]
public class GlobalModelInstaller : ScriptableObjectInstaller<GlobalModelInstaller>
{
    [SerializeField]
    private AssetsContext _legacyContext;
    [SerializeField]
    private Vector3Value _groundClick;
    [SerializeField]
    private DamageableValue _damageableObject;
    [SerializeField]
    private SelectableValue _selectableObject;
    // [SerializeField] 
    // private Sprite _chomperSprite;
    
    
    public override void InstallBindings()
    {
        Container.Bind<AssetsContext>().FromInstance(_legacyContext);
        Container.Bind<Vector3Value>().FromInstance(_groundClick);
        Container.Bind<DamageableValue>().FromInstance(_damageableObject);
        
        Container.Bind<IObservable<ISelectable>>().FromInstance(_selectableObject);
        
        Container.Bind<IAwaitable<IDamageable>>().FromInstance(_damageableObject);
        Container.Bind<IAwaitable<Vector3>>().FromInstance(_groundClick);
        
        //Container.Bind<Sprite>().WithId("Chomper").FromInstance(_chomperSprite);
        
        SceneManager.sceneUnloaded += SceneManagerOnSceneUnloaded;
    }
    
    private void SceneManagerOnSceneUnloaded(Scene arg0)
    {
        _selectableObject.ChangeValue(null);
        _damageableObject.ChangeValue(null);
    }
}