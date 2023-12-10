using System.Net.Sockets;

namespace LgtvNetworkController.Networking;

public interface ITVCommandClient
{
    Task<byte[]> ExecuteCommand(byte[] data);
}

public class TVNetworkClient : ITVCommandClient
{
    private readonly string host;
    private readonly int port;
    private readonly int timeout;

    public TVNetworkClient(string host, int port, int timeout)
    {
        this.host = host;
        this.port = port;
        this.timeout = timeout;
    }

    public async Task<byte[]> ExecuteCommand(byte[] data)
    {
        using var client = await CreateConnectedClient();
        await Write(client, data);
        var result = await Read(client);
        client.Close();

        return result;
    }

    private async Task<TcpClient> CreateConnectedClient()
    {
        var client = new TcpClient();
        await client.ConnectAsync(host, port);
        client.ReceiveTimeout = timeout;
        return client;
    }
    
    private static async Task<byte[]> Read(TcpClient client)
    {
        var buffer = new byte[1024];
        var stream = client.GetStream();
        var bytesRead = await stream.ReadAsync(buffer);
        return buffer.Take(bytesRead).ToArray();
    }

    private static async Task Write(TcpClient client, byte[] data)
    {
        var stream = client.GetStream();
        await stream.WriteAsync(data);
    }
}
