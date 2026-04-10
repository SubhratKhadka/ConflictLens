using Lafda.Enums;

namespace Lafda.Entities;

public class Theory : BaseEntity
{
    private int _upVoteCount;
    private int _downVoteCount;

    public PostStatusEnum PostStatus { get; set; } = PostStatusEnum.Waiting;
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int UpVoteCount
    {
        get => _upVoteCount;
        set =>
            _upVoteCount =
                value >= 0
                    ? value
                    : throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "Upvote cannot be negative."
                    );
    }

    public int DownVoteCount
    {
        get => _downVoteCount;
        set =>
            _downVoteCount =
                value >= 0
                    ? value
                    : throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "Downvote cannot be negative."
                    );
    }

    // foreign keys
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int MainEventId { get; set; }
    public MainEvent MainEvent { get; set; } = null!;
    public int? EventId { get; set; }
    public Event? Event { get; set; } = null;
}
