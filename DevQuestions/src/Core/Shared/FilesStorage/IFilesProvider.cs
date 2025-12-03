namespace Shared.FilesStorage;

public interface IFilesProvider
{
    public Task<string> UploadAsync(Stream fileStream, string key, string bucket);
    public Task<string> GetUrlByIdAsync(Guid fileId, CancellationToken cancellationToken);
    public Task<Dictionary<Guid, string>> GetUrlsByIdsAsync(IEnumerable<Guid> fileIds, CancellationToken cancellationToken); 
    
}