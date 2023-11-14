using UniRx;

namespace Abstractions.Items.Production
{
    public interface IUnitProducer
    {
        IReadOnlyReactiveCollection<IUnitProductionTask> Queue { get; }
        void Cancel(int index);
    }
}