using LgtvNetworkController.Commands.Commands;
using LgtvNetworkController.Networking;

namespace LgtvNetworkController.Commands.Handlers;

public class SetBacklightCommandHandler : ICommandHandler<SetBacklightCommand>
{
    private readonly ITVNetworkControlService networkControlService;

    public SetBacklightCommandHandler(ITVNetworkControlService networkControlService) =>
        this.networkControlService = networkControlService;

    public async Task<CommandResult> Handle(SetBacklightCommand command)
    {
        var result = await networkControlService.ExecuteCommand($"PICTURE_BACKLIGHT {command.Level}");
        // todo: verify result
        return new CommandResult(Enums.CommandResult.Success);
    }
}
