using Shared.Abstractions;
using Tags.Contracts.Dtos;
using Tags.Database;
using Tags.Features;

namespace Tags.Presenters;

public class TagModuleContract
{
    private readonly IQueryHandler<IReadOnlyList<TagDto>, GetByIds.GetByIdsQuery> _handler;
    private readonly TagsDbContext _tagsDbContext;


    public TagModuleContract(
        IQueryHandler<IReadOnlyList<TagDto>, GetByIds.GetByIdsQuery> handler,
        TagsDbContext tagsDbContext)
    {
        _handler = handler;
        _tagsDbContext = tagsDbContext;
    }

    public async Task CreateTag(CreateTagDto dto)
    {
        await Create.Handler(dto, _tagsDbContext);
    }

    public async Task<IReadOnlyList<TagDto>> GetByIds(GetByIdsDto dto, CancellationToken cancellationToken = default)
    {
        return await _handler.HandleAsync(new GetByIds.GetByIdsQuery(dto), cancellationToken);
    }
}