using Abstractions.Items;
using Zenject;

namespace Core.Items.Chomper
{
    public class ChomperInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IHealthHolder>().FromComponentInChildren();
            Container.Bind<float>().WithId("AttackDistance").FromInstance(5f);
            Container.Bind<int>().WithId("AttackPeriod").FromInstance(1400);
        }
    }
}