using Lafda.Enums;
using Pgvector;

namespace Lafda.Entities;
public class Event : BaseEntity
{
    public DisorderTypeEnum DisorderType {get;set;}
    // public
    public decimal Longitude {get; set;}
    public decimal Latitute {get; set;}
    public string Initiator {get; set;}
    public string Victim {get; set;}
    public Int32 Casualties {get; set;}


    public Vector Embedding {get; set;} = default!;
}