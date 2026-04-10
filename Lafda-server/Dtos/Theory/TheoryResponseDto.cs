namespace Lafda.Dtos;

using Lafda.Enums;


public class TheoryResponseDto
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    public PostStatusEnum PostStatus { get; set; }

    public int UpVoteCount { get; set; }
    public int DownVoteCount { get; set; }

    // relations
    public int UserId { get; set; }
    public string UserName { get; set; }

    public int MainEventId { get; set; }
    public string MainEventName { get; set; }

    public int? EventId { get; set; }
    public string? EventName { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}