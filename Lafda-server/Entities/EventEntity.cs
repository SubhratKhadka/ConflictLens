using System.ComponentModel.DataAnnotations;
using Lafda.Enums;
using Pgvector;

namespace Lafda.Entities;

public class Event : BaseEntity
{
    private int _humanCasualties;

    [RegularExpression("Political Violence")]
    public string DisorderType { get; set; } = "Political Violence";

    [RegularExpression("Battles|Explosions/Remote violence")]
    public required string EventType { get; set; }
    public required string SubEventType { get; set; }
    public required string Actor1 { get; set; }
    public string Actor2 { get; set; } = "";

    public string Notes { get; set; } = "";

    [Range(0, int.MaxValue)] // For UI/API Validation
    public int HumanCasualties
    {
        get => _humanCasualties;
        set =>
            _humanCasualties =
                value >= 0
                    ? value
                    : throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "Casualties cannot be negative."
                    );
    }

    public decimal Longitude { get; set; }
    public decimal Latitute { get; set; }
    public DateOnly EventDate { get; set; }

    // foreign keys
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int MainEventId { get; set; }
    public MainEvent MainEvent { get; set; } = null!;

    public PostStatusEnum EventStatus { get; set; } = PostStatusEnum.Waiting;
    // extra
    public Vector Embedding { get; set; } = default!;
}
