using LgtvNetworkController.Commands.Commands;
using System.Net;
using System.Net.Sockets;

namespace LgtvNetworkController.Commands.Handlers;

public class PowerOnCommandHandler : ICommandHandler<PowerOnCommand>
{
    public Task<CommandResult> Handle(PowerOnCommand command)
    {
        WakeOnLan(command.MacAddress);
        var result = new CommandResult(Enums.CommandResult.Success);
        return Task.FromResult(result);
    }

    private static void WakeOnLan(string macAddress)
    {
        var client = new UdpClient { EnableBroadcast = true };
        var payload = GenerateMagicPacket(macAddress);
        var endpoint = new IPEndPoint(IPAddress.Broadcast, 9);
        client.Send(payload, payload.Length, endpoint);
    }

    private static byte[] GenerateMagicPacket(string macAddress)
    {
        const int payloadSize = 102; // 6 bytes of FF followed by 16 repetitions of the MAC address (6 bytes each)
        var payload = new byte[payloadSize];
        var macBytes = macAddress
            .Split(':')
            .Select(x => Convert.ToByte(x, 16))
            .ToArray();

        // Set first 6 bytes to FF
        for (var i = 0; i < 6; i++)
        {
            payload[i] = 0xFF;
        }

        // Repeat MAC address 16 times
        for (var i = 6; i < payloadSize; i++)
        {
            payload[i] = macBytes[(i - 6) % macBytes.Length];
        }

        return payload;
    }
}
