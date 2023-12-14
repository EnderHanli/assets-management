namespace Domain.Common
{
    public interface IDeletionAudit
    {
        DateTime? DeletedDate { get; }
        string? DeletedBy { get; }
    }
}
