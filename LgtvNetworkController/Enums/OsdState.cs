namespace LgtvNetworkController.Enums;

public class OsdState
{
    public readonly string Value;

    public static readonly OsdState OsdOn = new("on");
    public static readonly OsdState OsdOff = new("off");

    private OsdState(string value) => Value = value;

    public override string ToString() => Value;
}
