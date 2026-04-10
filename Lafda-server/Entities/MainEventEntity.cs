namespace Lafda.Entities;

public class MainEvent : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
}
