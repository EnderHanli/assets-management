namespace Domain.Common
{
    public abstract class AuditableEntity : AuditableEntity<int>
    {
    }

    public abstract class AuditableEntity<TKey> : CreationAuditableEntity, IModificationAudit
    {
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
