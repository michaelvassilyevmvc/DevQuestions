using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions.Features.CreateQuestion;

public class CreateQuestionHandler: ICommandHandler<Guid, CreateQuestionCommand>
{
    private readonly ILogger<CreateQuestionHandler> _logger;
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<CreateQuestionDto> _validator;

    public CreateQuestionHandler(
        ILogger<CreateQuestionHandler> logger,
        IQuestionsRepository questionsRepository,
        IValidator<CreateQuestionDto> validator)
    {
        _logger = logger;
        _questionsRepository = questionsRepository;
        _validator = validator;
    }

    public async Task<Result<Guid, Failure>> HandleAsync(
        CreateQuestionCommand command,
        CancellationToken cancellationToken)
    {
        // валидация входных данных
        var validationResult = await _validator.ValidateAsync(command.QuestionDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            // throw new QuestionValidationException(validationResult.ToErrors());
            return validationResult.ToErrors();
        }

        // валидация бизнес логики
        var openUserQuestionsCount =
            await _questionsRepository.GetOpenUserQuestionsAsync(command.QuestionDto.UserId, cancellationToken);
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
            command.QuestionDto.Title,
            command.QuestionDto.Text,
            command.QuestionDto.UserId,
            null,
            command.QuestionDto.TagIds
        );
        // сохранение сущности Question в БД
        await _questionsRepository.AddAsync(question, cancellationToken);

        // Логирование об успешном или неуспешном сохранении
        _logger.LogInformation("Question created with id {QuestionId}", questionId);
        return questionId;
    }
}