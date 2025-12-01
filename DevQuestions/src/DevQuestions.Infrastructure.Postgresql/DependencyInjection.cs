using DevQuestions.Application.Database;
using DevQuestions.Application.Questions;
using DevQuestions.Infrastructure.Postgresql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuestions.Infrastructure.Postgresql;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresInfrastructure(this IServiceCollection services)
    {
        // services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IQuestionsRepository, QuestionsEFCoreRepository>();
        services.AddDbContext<QuestionsDbContext>();
        return services;
    }
}