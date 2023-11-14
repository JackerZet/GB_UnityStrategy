namespace Abstractions.Items
{
    public interface IDamageable : IHealthHolder
    {
        void ReceiveDamage(int amount);
    }
}