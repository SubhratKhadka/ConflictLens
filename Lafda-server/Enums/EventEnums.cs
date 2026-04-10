using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lafda.Enums;

public enum DisorderTypeEnum
{
    // Demonstrations = 0,
    // StrategicDevelopments = 1,
    // PoliticalViolence = 2
    [Description("Political Violence")]
    [Display(Name = "Political Violence")]
    PoliticalViolence = 0,
}