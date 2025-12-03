using CSharpFunctionalExtensions;
using Shared;
using Shared.Abstractions;

namespace Questions.Application.Features.SelectSolutionCommand;

public record SelectSolutionCommand() : ICommand;

public class SelectSolutionCommandHandler : ICommandHandler<Guid, SelectSolutionCommand>
{
    public Task<Result<Guid, Failure>> HandleAsync(SelectSolutionCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
}