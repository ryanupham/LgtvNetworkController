namespace LgtvNetworkController.Configuration;

public interface ITVConnectionConfiguration
{
    string Key { get; }
    string MacAddress { get; }
    string Host { get; }
    int NetworkPort { get; }
    int NetworkTimeout { get; }
}

public record TVConnectionConfiguration : ITVConnectionConfiguration
{
    public string Key { get; init; } = "";
    public string MacAddress { get; init; } = "";
    public string Host { get; init; } = "";
    public int NetworkPort { get; init; }
    public int NetworkTimeout { get; init; }

    public static TVConnectionConfiguration GetDefault() =>
        new()
        {
            Key = "",
            MacAddress = "",
            Host = "",
            NetworkPort = 9761,
            NetworkTimeout = 5000,
        };
}
