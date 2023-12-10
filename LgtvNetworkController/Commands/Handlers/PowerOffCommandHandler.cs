using LgtvNetworkController.Commands.Commands;
using LgtvNetworkController.Networking;

namespace LgtvNetworkController.Commands.Handlers;

public class PowerOffCommandHandler : ICommandHandler<PowerOffCommand>
{
    private readonly ITVNetworkControlService networkControlService;

    public PowerOffCommandHandler(ITVNetworkControlService networkControlService) =>
        this.networkControlService = networkControlService;

    public async Task<CommandResult> Handle(PowerOffCommand command)
    {
        var result = await networkControlService.ExecuteCommand("POWER toggle");
        // todo : check result validity
        return new CommandResult(Enums.CommandResult.Success);
    }
}
