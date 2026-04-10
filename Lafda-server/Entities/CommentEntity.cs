using System.ComponentModel.DataAnnotations;

namespace Lafda.Entities;

public class Comment : BaseEntity
{
    public required string Message { get; set; }

    // foreign keys
    public int TheoryId { get; set; }
    public Theory Theory { get; set; } = null!;
}
