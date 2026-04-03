namespace Lafda.Entities;

using Lafda.Enums;
using System.Numerics;
using Pgvector;

public class Event : BaseEntity
{
    public DisorderTypeEnum DisorderType {get;set;}
    // public
    public Vector Embedding {get; set;} = default!;
}