using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Extensions;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions.Features.AddAnswerCommand;

public class AddAnswerCommandHandler: ICommandHandler<Guid, AddAnswerCommand>
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger<AddAnswerCommandHandler> _logger;
    private readonly IValidator<AddAnswerDto> _addAnswerDtoValidator;
    // private readonly ITransactionManager _transactionManager;
    // private readonly IUserCommunicationService _userCommunicationService;

    public AddAnswerCommandHandler(
        IQuestionsRepository questionsRepository,
        ILogger<AddAnswerCommandHandler> logger,
        IValidator<AddAnswerDto> addAnswerDtoValidator)
    {
        _questionsRepository = questionsRepository;
        _logger = logger;
        _addAnswerDtoValidator = addAnswerDtoValidator;
    }

    public async Task<Result<Guid, Failure>> HandleAsync(
        AddAnswerCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _addAnswerDtoValidator.ValidateAsync(command.AddAnswerDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        // var userRatingResult =
        //     await _userCommunicationService.GetUserRatingAsync(command.AddAnswerDto.UserId, cancellationToken);
        // if (userRatingResult.IsFailure)
        // {
        //     return userRatingResult.Error;
        // }
        //
        // if (userRatingResult.Value <= 0)
        // {
        //     _logger.LogError("User with id {userId} has no rating", command.AddAnswerDto.UserId);
        //     return Errors.Questions.NotEnoughRating();
        // }

        // var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);

        (_, bool isFailure, Question? question, Failure? error) =
            await _questionsRepository.GetByIdAsync(command.QuestionId, cancellationToken);
        if (isFailure)
        {
            return error;
        }


        var answer = new Answer(new Guid(), command.AddAnswerDto.UserId, command.AddAnswerDto.Text, command.QuestionId);

        question?.Answers.Add(answer);
        await _questionsRepository.SaveAsync(question, cancellationToken);

        // transaction.Commit();

        _logger.LogInformation("Adding answer with id = {answerId} to question {questionId}", answer.Id, command.QuestionId);
        return answer.Id;
    }
}