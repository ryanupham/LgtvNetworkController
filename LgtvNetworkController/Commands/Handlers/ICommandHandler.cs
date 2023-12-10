namespace LgtvNetworkController.Commands.Handlers;

public interface ICommandHandler { }

public interface ICommandHandler<in TCommand> : ICommandHandler
{
    Task<CommandResult> Handle(TCommand command);
}
