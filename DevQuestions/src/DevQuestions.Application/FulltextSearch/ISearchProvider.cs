using DevQuestions.Domain.Questions;

namespace DevQuestions.Application.FulltextSearch;

public interface ISearchProvider
{
    Task<List<Guid>> SearchAsync(string query);
    Task IndexQuestionAsync(Question question);
}