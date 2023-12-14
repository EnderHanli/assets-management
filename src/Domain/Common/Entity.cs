namespace Domain.Common
{
    public abstract class Entity : Entity<int>
    {
    }

    public abstract class Entity<TKey>
    {
        public TKey Id { get; init; }
    }
}
