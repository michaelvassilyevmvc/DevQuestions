namespace DevQuestions.Infrastructure.Postgresql.Seeders;

public class QuestionsSeeder: ISeeder
{
    private readonly QuestionsDbContext _dbContext;

    public QuestionsSeeder(QuestionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SeedAsync()
    {
        throw new NotImplementedException();
    }
}