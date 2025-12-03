using CSharpFunctionalExtensions;
using DevQuestions.Application.Questions;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Application.Questions.Features.GetQuestionsWithFiltersQuery;
using DevQuestions.Domain.Questions;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace DevQuestions.Infrastructure.Postgresql.Questions;

public class QuestionsEFCoreRepository : IQuestionsRepository
{
    private readonly QuestionsReadDbContext _readDbContext;

    public QuestionsEFCoreRepository(QuestionsReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        await _readDbContext.Questions.AddAsync(question, cancellationToken);
        await _readDbContext.SaveChangesAsync(cancellationToken);
        return question.Id;
    }

    public async Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken)
    {
        _readDbContext.Questions.Attach(question);
        await _readDbContext.SaveChangesAsync(cancellationToken);
        return question.Id;
    }


    public Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public async Task<Result<Question?, Failure>> GetByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        var question = await _readDbContext.Questions
            .Include(q => q.Answers)
            .Include(q => q.Solution)
            .FirstOrDefaultAsync(q => q.Id == questionId);

        if (question is null)
        {
            return Errors.General.NotFound(questionId)
                .ToFailure();
        }

        return question;
    }

    public async Task<int> GetOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<(IReadOnlyList<Question> Questions,long Count)> GetQuestionsWithFiltersAsync(GetQuestionsWithFiltersQuery query,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> AddAnswerAsync(Answer answer, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}