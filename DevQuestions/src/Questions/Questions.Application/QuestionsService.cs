using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Questions.Application.Fails;
using Questions.Contracts.Dtos;
using Questions.Domain;
using Shared;
using Shared.Database;
using Shared.Extensions;

namespace Questions.Application;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger<QuestionsService> _logger;
    private readonly IValidator<CreateQuestionDto> _createQuestionDtoValidator;
    private readonly IValidator<AddAnswerDto> _addAnswerDtoValidator;
    private readonly ITransactionManager _transactionManager;
    // private readonly IUserCommunicationService _userCommunicationService;

    public QuestionsService(
        IQuestionsRepository questionsRepository,
        ILogger<QuestionsService> logger,
        IValidator<CreateQuestionDto> createQuestionDtoValidator,
        IValidator<AddAnswerDto> addAnswerDtoValidator,
        ITransactionManager transactionManager
        // IUserCommunicationService userCommunicationService
        )
    {
        _questionsRepository = questionsRepository;
        _logger = logger;
        _createQuestionDtoValidator = createQuestionDtoValidator;
        _addAnswerDtoValidator = addAnswerDtoValidator;
        _transactionManager = transactionManager;
        // _userCommunicationService = userCommunicationService;
    }

    public async Task<Result<Guid, Failure>> Create(
        CreateQuestionDto questionDto,
        CancellationToken cancellationToken)
    {
        // валидация входных данных
        var validationResult = await _createQuestionDtoValidator.ValidateAsync(questionDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            // throw new QuestionValidationException(validationResult.ToErrors());
            return validationResult.ToErrors();
        }

        // валидация бизнес логики
        var openUserQuestionsCount =
            await _questionsRepository.GetOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);
        if (openUserQuestionsCount > 3)
        {
            // throw new ToManyQuestionsException();
            return Errors.Questions.ToManyQuestions()
                .ToFailure();
        }

        // создание сущности
        var questionId = Guid.NewGuid();
        var question = new Question(
            questionId,
            questionDto.Title,
            questionDto.Text,
            questionDto.UserId,
            null,
            questionDto.TagIds
        );
        // сохранение сущности Question в БД
        await _questionsRepository.AddAsync(question, cancellationToken);

        // Логирование об успешном или неуспешном сохранении
        _logger.LogInformation("Question created with id {QuestionId}", questionId);
        return questionId;
    }

    public async Task<Result<Guid, Failure>> AddAnswer(
        Guid questionId,
        AddAnswerDto addAnswerDto,
        CancellationToken cancellationToken)
    {
        var validationResult = await _addAnswerDtoValidator.ValidateAsync(addAnswerDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        // var userRatingResult =
        //     await _userCommunicationService.GetUserRatingAsync(addAnswerDto.UserId, cancellationToken);
        // if (userRatingResult.IsFailure)
        // {
        //     return userRatingResult.Error;
        // }
        //
        // if (userRatingResult.Value <= 0)
        // {
        //     _logger.LogError("User with id {userId} has no rating", addAnswerDto.UserId);
        //     return Errors.Questions.NotEnoughRating();
        // }

        var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);

        (_, bool isFailure, Question? question, Failure? error) =
            await _questionsRepository.GetByIdAsync(questionId, cancellationToken);
        if (isFailure)
        {
            return error;
        }


        var answer = new Answer(new Guid(), addAnswerDto.UserId, addAnswerDto.Text, questionId);

        question?.Answers.Add(answer);
        await _questionsRepository.SaveAsync(question, cancellationToken);

        transaction.Commit();

        _logger.LogInformation("Adding answer with id = {answerId} to question {questionId}", answer.Id, questionId);
        return answer.Id;
    }
}