namespace Lafda.Dtos;

using Lafda.Enums;
public class TheoryListDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public PostStatusEnum PostStatus { get; set; }

    public int UpVoteCount { get; set; }
    public int DownVoteCount { get; set; }

    public string UserName { get; set; }

    public DateTime CreatedAt { get; set; }
}