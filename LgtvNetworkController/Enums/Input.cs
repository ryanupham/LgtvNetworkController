namespace LgtvNetworkController.Enums;

public class Input
{
    public readonly string Value;

    public static readonly Input Atv = new("atv");
    public static readonly Input AV = new("avav1");
    public static readonly Input Cadtv = new("cadtv");
    public static readonly Input Catv = new("catv");
    public static readonly Input Component = new("component1");
    public static readonly Input Dtv = new("dtv");
    public static readonly Input Hdmi1 = new("hdmi1");
    public static readonly Input Hdmi2 = new("hdmi2");
    public static readonly Input Hdmi3 = new("hdmi3");
    public static readonly Input Hdmi4 = new("hdmi4");

    private Input(string value) => Value = value;

    public override string ToString() => Value;
}
