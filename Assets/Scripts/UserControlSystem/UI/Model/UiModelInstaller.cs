using Abstractions.Commands.CommandInterfaces;
using UserControlSystem.UI.Model.CommandCreators;
using Zenject;

namespace UserControlSystem.UI.Model
{
    public class UiModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>()
                .To<ProduceUnitCommandCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IAttackCommand>>()
                .To<AttackCommandCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IMoveCommand>>()
                .To<MoveCommandCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>()
                .To<PatrolCommandCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IStopCommand>>()
                .To<StopCommandCommandCreator>().AsTransient();

            Container.Bind<CommandButtonsModel>().AsTransient();
            Container.BindInterfacesAndSelfTo<BottomCenterModel>().AsSingle();

            //Container.Bind<float>().WithId("Chomper").FromInstance(5f);
            //Container.Bind<string>().WithId("Chomper").FromInstance("Chomper");
        }
    }
}