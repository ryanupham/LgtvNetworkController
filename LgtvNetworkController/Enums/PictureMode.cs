namespace LgtvNetworkController.Enums;

public class PictureMode
{
    public readonly string Value;
    
    public static readonly PictureMode cinema = new("cinema");
    public static readonly PictureMode eco = new("eco");
    public static readonly PictureMode filmMaker = new("filmMaker");
    public static readonly PictureMode game = new("game");
    public static readonly PictureMode normal = new("normal");
    public static readonly PictureMode sports = new("sports");
    public static readonly PictureMode vivid = new("vivid");

    private PictureMode(string value) => Value = value;

    public override string ToString() => Value;
}
