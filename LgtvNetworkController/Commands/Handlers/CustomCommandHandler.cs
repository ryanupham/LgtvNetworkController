using LgtvNetworkController.Commands.Commands;
using LgtvNetworkController.Networking;

namespace LgtvNetworkController.Commands.Handlers;

public class CustomCommandHandler : ICommandHandler<CustomCommand>
{
    private readonly ITVNetworkControlService networkControlService;

    public CustomCommandHandler(ITVNetworkControlService networkControlService) =>
        this.networkControlService = networkControlService;

    public async Task<CommandResult> Handle(CustomCommand command)
    {
        var result = await networkControlService.ExecuteCommand(command.Command);
        // todo: verify result
        return new CommandResult(Enums.CommandResult.Success);
    }
}
