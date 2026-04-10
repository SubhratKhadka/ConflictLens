namespace Lafda.Dtos;
using Lafda.Enums;

public class EventResponseDto
{
    public int Id { get; set; }

    public DisorderTypeEnum DisorderType { get; set; }

    public string EventType { get; set; }
    public string SubEventType { get; set; }

    public string Actor1 { get; set; }
    public string Actor2 { get; set; }

    public string Notes { get; set; }

    public int HumanCasualties { get; set; }

    public decimal Longitude { get; set; }
    public decimal Latitute { get; set; }

    public DateOnly EventDate { get; set; }

    // relations
    public int UserId { get; set; }
    public string UserName { get; set; }

    public int MainEventId { get; set; }
    public string MainEventTitle { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}