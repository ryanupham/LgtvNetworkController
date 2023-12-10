using System.Collections.Concurrent;
using LgtvNetworkController.Commands.Handlers;
using LgtvNetworkController.Utilities;

namespace LgtvNetworkController.Commands.Decorators;

public class OnlyHandleLatestCommandHandlerDecorator<TCommand>
    : ICommandHandler<TCommand>
{
    private readonly ICommandHandler<TCommand> decoratedCommandHandler;
    private readonly long executionNumber;

    private static readonly ConcurrentDictionary<Type, AtomicCounter>
        executionCounters = new();

    public OnlyHandleLatestCommandHandlerDecorator(
        ICommandHandler<TCommand> decoratedCommandHandler)
    {
        this.decoratedCommandHandler = decoratedCommandHandler;
        executionNumber = GetExecutionCounter().Increment();
    }

    public async Task<CommandResult> Handle(TCommand command)
    {
        var executionCount = GetExecutionCounter().Value;
        if (executionNumber < executionCount)
        {
            return new CommandResult(Enums.CommandResult.Canceled);
        }

        await decoratedCommandHandler.Handle(command);
        return new CommandResult(Enums.CommandResult.Success);
    }

    private static AtomicCounter GetExecutionCounter() =>
        executionCounters.GetOrAdd(typeof(TCommand), _ => new AtomicCounter());
}
