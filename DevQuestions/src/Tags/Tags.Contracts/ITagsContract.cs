using Tags.Contracts.Dtos;

namespace Tags.Contracts;

public interface ITagsContract
{
    Task<Guid> CreateTag(CreateTagDto dto);
    Task<IReadOnlyList<TagDto>> GetByIds(GetByIdsDto dto);
}