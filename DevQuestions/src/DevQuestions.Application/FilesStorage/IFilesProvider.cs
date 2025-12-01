namespace DevQuestions.Application.FilesStorage;

public interface IFilesProvider
{
    public Task<string> UploadAsync(Stream fileStream, string key, string bucket);
}