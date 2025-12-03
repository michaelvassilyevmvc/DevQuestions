using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using Shared;

namespace DevQuestions.Application.Questions.Features.SelectSolutionCommand;

public record SelectSolutionCommand() : ICommand;

public class SelectSolutionCommandHandler : ICommandHandler<Guid, SelectSolutionCommand>
{
    public Task<Result<Guid, Failure>> HandleAsync(SelectSolutionCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
}