using Lafda.Enums;

namespace Lafda.Entities;

public class Theory : BaseEntity
{
    public PostStatusEnum PostStatus {get; set;} = PostStatusEnum.Waiting;
    
}