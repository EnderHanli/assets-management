namespace Domain.Common
{
    public abstract class CreationAuditableEntity : CreationAuditableEntity<int>
    {
    }

    public abstract class CreationAuditableEntity<TKey> : Entity<TKey>, ICreationAudit
    {
        public DateTime CreatedDate { get; set; }

        public string? CreatedBy { get; set; }
    }
}
