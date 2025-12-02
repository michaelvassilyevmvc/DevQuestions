using CSharpFunctionalExtensions;
using DevQuestions.Application.FulltextSearch;
using DevQuestions.Domain.Questions;
using Shared;

namespace DevQuestions.Infrastructure.ElasticSearch;

public class ElasticSearchProvider : ISearchProvider
{
    public Task<List<Guid>> SearchAsync(string query) => throw new NotImplementedException();

    public async Task<UnitResult<Failure>> IndexQuestionAsync(Question question)
    {
        try
        {
            // _elastic.Search();
        }
        catch (Exception ex)
        {
            return Error.Failure("search.error", ex.Message)
                .ToFailure();
        }

        return UnitResult.Success<Failure>();
    }
}