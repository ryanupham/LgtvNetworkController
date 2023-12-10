using LgtvNetworkController.Commands.Commands;
using LgtvNetworkController.Commands.Decorators;
using LgtvNetworkController.Commands.Handlers;
using LgtvNetworkController.Commands;
using LgtvNetworkController.Configuration;
using LgtvNetworkController.Networking;
using Microsoft.Extensions.DependencyInjection;

namespace LgtvNetworkController.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandDispatcher(
        this IServiceCollection services, ITVConnectionConfiguration settings) =>
        services
            .AddSingleton<ITVNetworkControlService, TVNetworkControlService>()
            .AddSingleton<ITVCommandClient, TVNetworkClient>(sp =>
                new TVNetworkClient(
                    settings.Host,
                    settings.NetworkPort,
                    settings.NetworkTimeout))
            .AddSingleton<IMessageCodec, MessageCodec>(
                sp => MessageCodec.Create(o => o.Key = settings.Key))
            .AddTransient<ICommandHandler<PowerOffCommand>, PowerOffCommandHandler>()
            .AddTransient<ICommandHandler<CustomCommand>, CustomCommandHandler>()
            .AddTransient<ICommandHandler<SetBacklightCommand>, SetBacklightCommandHandler>()
            .AddDecorator<ICommandHandler<SetBacklightCommand>, OnlyHandleLatestCommandHandlerDecorator<SetBacklightCommand>>()
            .AddTransient<ICommandHandler<SetOsdStateCommand>, SetOsdStateCommandHandler>()
            .AddSingleton<ICommandDispatcher, CommandDispatcher>();
}
