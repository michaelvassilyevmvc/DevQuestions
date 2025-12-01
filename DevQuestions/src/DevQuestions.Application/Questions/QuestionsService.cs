using DevQuestions.Application.Extensions;
using DevQuestions.Application.FulltextSearch;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Application.Questions.Fails.Exceptions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger<QuestionsService> _logger;
    private readonly ISearchProvider _searchProvider;
    private readonly IValidator<CreateQuestionDto> _validator;

    public QuestionsService(
        IQuestionsRepository questionsRepository,
        ILogger<QuestionsService> logger,
        ISearchProvider searchProvider,
        IValidator<CreateQuestionDto> validator)
    {
        _questionsRepository = questionsRepository;
        _logger = logger;
        _validator = validator;
        _searchProvider = searchProvider;
    }

    public async Task<Guid> Create(
        CreateQuestionDto questionDto,
        CancellationToken cancellationToken)
    {
        // валидация входных данных
        var validationResult = await _validator.ValidateAsync(questionDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new QuestionValidationException(validationResult.ToErrors());
        }

        // валидация бизнес логики
        var openUserQuestionsCount =
            await _questionsRepository.GetOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);
        if (openUserQuestionsCount > 3)
        {
            throw new ToManyQuestionsException();
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

        await _searchProvider.IndexQuestionAsync(question);
        // Логирование об успешном или неуспешном сохранении
        _logger.LogInformation("Question created with id {QuestionId}", questionId);
        return questionId;
    }
}