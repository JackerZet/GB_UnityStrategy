using Zenject;

namespace UserControlSystem.UI.View
{
    public class UiViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<BottomCenterView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}