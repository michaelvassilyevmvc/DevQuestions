using CSharpFunctionalExtensions;
using Dapper;
using DevQuestions.Application.Database;
using DevQuestions.Application.Questions;
using DevQuestions.Application.Questions.Features.GetQuestionsWithFiltersQuery;
using DevQuestions.Domain.Questions;
using Shared;

namespace DevQuestions.Infrastructure.Postgresql.Questions;

public class QuestionsSqlRepository : IQuestionsRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public QuestionsSqlRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        const string sql = """
                           INSERT INTO questions (id, title, text, user_id, screenshot_id, tags) 
                           VALUES (@id, @title, @text, @userId, @screenshotId, @tags)
                           """;

        using var connection = _sqlConnectionFactory.Create();
        await connection.ExecuteAsync(sql,
            new
            {
                Id = question.Id,
                Title = question.Title,
                Text = question.Text,
                UserId = question.UserId,
                ScreenshotId = question.ScreenshotId,
                Tags = question.Tags.ToArray()
            });

        return question.Id;
    }

    public Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<Result<Question?, Failure>> IQuestionsRepository.GetByIdAsync(Guid questionId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Question?> GetByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<(IReadOnlyList<Question> Questions, long Count)> GetQuestionsWithFiltersAsync(GetQuestionsWithFiltersQuery query, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Guid> AddAnswerAsync(Answer answer, CancellationToken cancellationToken) => throw new NotImplementedException();
}