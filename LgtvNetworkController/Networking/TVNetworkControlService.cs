namespace LgtvNetworkController.Networking;

public interface ITVNetworkControlService
{
    Task<string> ExecuteCommand(string command);
}

public class TVNetworkControlService : ITVNetworkControlService
{
    private readonly IMessageCodec messageCodec;
    private readonly ITVCommandClient tvCommandClient;

    public TVNetworkControlService(IMessageCodec messageCodec, ITVCommandClient tvCommandClient)
    {
        this.messageCodec = messageCodec;
        this.tvCommandClient = tvCommandClient;
    }

    public async Task<string> ExecuteCommand(string command)
    {
        var encryptedCommand = messageCodec.Encrypt(command);

        var encryptedResponse = await tvCommandClient.ExecuteCommand(encryptedCommand);
        return messageCodec.Decrypt(encryptedResponse);
    }
}
