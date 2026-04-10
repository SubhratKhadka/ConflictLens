namespace Lafda.Dtos;

public class CreateTheoryDto
{
    public string Title { get; set; }
    public string Description { get; set; }

    public int MainEventId { get; set; }
    public int? EventId { get; set; }
}