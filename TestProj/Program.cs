using LgtvNetworkController.Commands;
using LgtvNetworkController.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LgtvNetworkController.DependencyInjection.Extensions;
using LgtvNetworkController.Commands.Commands;
using System.Reflection;

namespace LgtvNetworkController;

internal class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No command provided.");
            return;
        }

        var settings = TVConnectionConfiguration.GetDefault() with
        {
            Host = "",
            MacAddress = "",
            Key = "",
        };

        var services = new ServiceCollection()
            .AddCommandDispatcher(settings)
            .BuildServiceProvider();
        var commandDispatcher = services.GetRequiredService<ICommandDispatcher>();
        var commandName = args[0] + "Command";
        var commandArgs = args[1..];

        try
        {
            var commandType = Type.GetType(commandName);
            if (commandType is null)
            {
                Console.WriteLine($"Command '{commandName}' not found.");
                return;
            }

            var commandInstance = Activator.CreateInstance(commandType, commandArgs)
                ?? throw new Exception($"Command '{commandName}' could not be initialized");
            await commandDispatcher.DispatchQueued(commandInstance);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating command instance: {ex.Message}");
        }

        var setBacklightCommand = new SetBacklightCommand(70);

        await commandDispatcher.DispatchQueued(setBacklightCommand);
    }

    private static object? CreateCommandInstance(string commandName, string[] args)
    {
        var commandType = Assembly.GetExecutingAssembly().GetTypes()
            .FirstOrDefault(t => t.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase));

        if (commandType is null)
        {
            return null;
        }

        try
        {
            return Activator.CreateInstance(commandType, args);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating command instance: {ex.Message}");
            return null;
        }
    }
}
