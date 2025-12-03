using CSharpFunctionalExtensions;
using Questions.Domain;
using Shared;
using Shared.FulltextSearch;

namespace Questions.Application.FulltextSearch;

public class SearchProvider : ISearchProvider
{
    public Task<List<Guid>> SearchAsync(string query) => throw new NotImplementedException();

    public Task<UnitResult<Failure>> IndexQuestionAsync<Question>(Question question, string indexName) =>
        throw new NotImplementedException();
}