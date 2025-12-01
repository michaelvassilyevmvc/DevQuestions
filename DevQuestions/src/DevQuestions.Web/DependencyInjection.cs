using DevQuestions.Application;
using DevQuestions.Infrastructure.Postgresql;

namespace DevQuestions.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection services)
        => services
            .AddWebDependencies()
            .AddApplication()
            .AddPostgresInfrastructure();

    private static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();
        return services;
    }
}