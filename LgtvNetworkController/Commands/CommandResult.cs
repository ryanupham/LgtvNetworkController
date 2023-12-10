namespace LgtvNetworkController.Commands;

public record CommandResult(Enums.CommandResult Result);

public record CommandResult<TResult>(Enums.CommandResult Result, TResult Value) : CommandResult(Result);
