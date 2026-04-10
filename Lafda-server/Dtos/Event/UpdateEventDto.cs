namespace Lafda.Dtos;
using Lafda.Enums;

public class UpdateEventDto
{
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

    public int MainEventId { get; set; }
}