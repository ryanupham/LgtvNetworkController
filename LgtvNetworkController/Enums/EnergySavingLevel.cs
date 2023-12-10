namespace LgtvNetworkController.Enums;

public class EnergySavingLevel
{
    public readonly string Value;

    public static readonly EnergySavingLevel Auto = new("auto");
    public static readonly EnergySavingLevel ScreenOffMinimum = new("off");
    public static readonly EnergySavingLevel ScreenOff = new("screenoff");
    public static readonly EnergySavingLevel Minimum = new("minimum");
    public static readonly EnergySavingLevel Medium = new("medium");
    public static readonly EnergySavingLevel Maximum = new("maximum");

    private EnergySavingLevel(string value) => Value = value;
    
    public override string ToString() => Value;
}
