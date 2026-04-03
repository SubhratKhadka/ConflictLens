using System.ComponentModel.DataAnnotations;
using Lafda.Enums;

namespace Lafda.Entities;


public class BaseEntity
{
    [Key] public int Id { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Active;
    public DateTime CreatedAt {get; set; } = DateTime.Now.ToUniversalTime();
    public DateTime UpdatedAt {get; set; } = DateTime.Now.ToUniversalTime();
}