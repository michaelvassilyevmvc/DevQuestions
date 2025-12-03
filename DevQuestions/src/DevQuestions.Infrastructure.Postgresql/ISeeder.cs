namespace DevQuestions.Infrastructure.Postgresql;

public interface ISeeder
{
    Task SeedAsync();
}