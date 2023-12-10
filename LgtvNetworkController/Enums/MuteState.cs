namespace LgtvNetworkController.Enums;

public class MuteState
{
    public readonly string Value;

    public static readonly MuteState MuteOn = new("on");
    public static readonly MuteState MuteOff = new("off");

    private MuteState(string value) => Value = value;

    public override string ToString() => Value;
}
