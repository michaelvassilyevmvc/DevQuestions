using DevQuestions.Application;
using DevQuestions.Infrastructure.Postgresql;
using Questions.Presenters;
using Tags;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services)
        => services
            .AddWebDependencies()
            .AddQuestionsModule()
            .AddTagsModule();

    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();
        return services;
    }
}