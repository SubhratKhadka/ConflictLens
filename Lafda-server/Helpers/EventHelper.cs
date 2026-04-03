using Lafda.Enums;

public static class DisorderTypeExtensions
{
    public static string ToDisplayString(this DisorderTypeEnum type)
    {
        return type switch
        {
            DisorderTypeEnum.Demonstrations => "Demonstrations",
            DisorderTypeEnum.StrategicDevelopments => "Strategic developments",
            DisorderTypeEnum.PoliticalViolence => "political violence",
            _ => "Unknown",
        };
    }
}
