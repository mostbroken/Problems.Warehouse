namespace Problems.Warehouse.Domain
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; }
    }
}