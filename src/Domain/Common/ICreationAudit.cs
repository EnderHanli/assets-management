namespace Domain.Common
{
    public interface ICreationAudit
    {
        DateTime CreatedDate { get; }
        string? CreatedBy { get; }
    }
}
