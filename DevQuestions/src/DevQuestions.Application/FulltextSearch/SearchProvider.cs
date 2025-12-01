using DevQuestions.Domain.Questions;

namespace DevQuestions.Application.FulltextSearch;

public class SearchProvider: ISearchProvider
{
    public Task<List<Guid>> SearchAsync(string query) => throw new NotImplementedException();

    public Task IndexQuestionAsync(Question question) => throw new NotImplementedException();
}