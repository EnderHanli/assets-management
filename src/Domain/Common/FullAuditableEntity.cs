namespace Domain.Common
{
    public abstract class FullAuditableEntity : FullAuditableEntity<int>
    {
    }

    public abstract class FullAuditableEntity<TKey> : AuditableEntity<TKey>, IDeletionAudit, ISoftDelete
    {
        public DateTime? DeletedDate { get; set; }

        public string? DeletedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
