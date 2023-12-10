namespace LgtvNetworkController.Enums;

public class CommandResult
{
    public readonly string Value;

    public static readonly CommandResult Success = new("success");
    public static readonly CommandResult Failure = new("failure");
    public static readonly CommandResult Canceled = new("canceled");

    private CommandResult(string value) => Value = value;

    public override string ToString() => Value;
}
