using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using Shared;

namespace DevQuestions.Application.Questions.Features.SelectSolution;

public record SelectSolutionCommand() : ICommand;

public class SelectSolutionHandler : ICommandHandler<Guid, SelectSolutionCommand>
{
    public Task<Result<Guid, Failure>> HandleAsync(SelectSolutionCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
}