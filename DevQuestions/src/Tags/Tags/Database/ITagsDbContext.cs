using Tags.Domain;

namespace Tags.Database;

public interface ITagsDbContext
{
    IQueryable<Tag> TagsRead { get; }
}