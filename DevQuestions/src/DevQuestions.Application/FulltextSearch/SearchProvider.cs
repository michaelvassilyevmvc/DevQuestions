using CSharpFunctionalExtensions;
using DevQuestions.Domain.Questions;
using Shared;

namespace DevQuestions.Application.FulltextSearch;

public class SearchProvider: ISearchProvider
{
    public Task<List<Guid>> SearchAsync(string query) => throw new NotImplementedException();

    public Task<UnitResult<Failure>> IndexQuestionAsync(Question question) => throw new NotImplementedException();
}