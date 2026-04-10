namespace Lafda.Dtos;

public class EventListDto
{
    public int Id { get; set; }

    public string EventType { get; set; }
    public string SubEventType { get; set; }

    public int HumanCasualties { get; set; }

    public DateOnly EventDate { get; set; }

    public string Actor1 { get; set; }

    public DateTime CreatedAt { get; set; }
}