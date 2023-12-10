using LgtvNetworkController.Commands;
using LgtvNetworkController.Commands.Commands;
using LgtvNetworkController.Networking;

namespace LgtvNetworkController.Commands.Handlers;

public class SetOsdStateCommandHandler : ICommandHandler<SetOsdStateCommand>
{
    private readonly ITVNetworkControlService networkControlService;

    public SetOsdStateCommandHandler(ITVNetworkControlService networkControlService) =>
        this.networkControlService = networkControlService;

    public async Task<CommandResult> Handle(SetOsdStateCommand command)
    {
        var result = await networkControlService.ExecuteCommand($"OSD_SELECT {command.State}");
        // todo: validate result
        return new CommandResult(Enums.CommandResult.Success);
    }
}