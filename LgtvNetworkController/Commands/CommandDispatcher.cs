using LgtvNetworkController.Commands.Handlers;
using LgtvNetworkController.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace LgtvNetworkController.Commands;

public interface ICommandDispatcher
{
    Task DispatchQueued<TCommand>(TCommand command) where TCommand : notnull;
}

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider serviceProvider;
    private readonly ConcurrentWaitingQueue waitingQueue = new();

    public CommandDispatcher(IServiceProvider serviceProvider) =>
        this.serviceProvider = serviceProvider;

    public async Task DispatchQueued<TCommand>(TCommand command)
        where TCommand : notnull
    {
        if (!TryGetHandler<TCommand>(out var handler))
        {
            throw new InvalidOperationException(
                $"No handler found for command of type {typeof(TCommand)}");
        }

        await waitingQueue.Enqueue();
        await handler!.Handle(command);
        waitingQueue.Dequeue();
    }

    private bool TryGetHandler<TCommand>(out ICommandHandler<TCommand>? handler)
    {
        handler = serviceProvider.GetService<ICommandHandler<TCommand>>();
        return handler is not null;
    }
}
