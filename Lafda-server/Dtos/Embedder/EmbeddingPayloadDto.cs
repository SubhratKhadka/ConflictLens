namespace Lafda.Dtos;

using System.ComponentModel.DataAnnotations;

public class EmbeddingPayloadDto
{
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public string Actor1 { get; set; } = null!;

    public string Actor2 { get; set; } = "";

    [Required]
    public string EventType { get; set; } = null!;

    [Required]
    public string SubEventType { get; set; } = null!;

    [Required]
    public DateOnly EventDate { get; set; }
}