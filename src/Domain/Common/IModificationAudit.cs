namespace Domain.Common
{
    public interface IModificationAudit
    {
        DateTime? LastModifiedDate { get; }
        string? LastModifiedBy { get; }
    }
}
