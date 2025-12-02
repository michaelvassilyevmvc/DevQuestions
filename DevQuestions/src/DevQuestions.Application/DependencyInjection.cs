using DevQuestions.Application.Abstractions;
using DevQuestions.Application.FulltextSearch;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuestions.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddValidatorsFromAssembly(assembly);
        services.AddScoped<ISearchProvider, SearchProvider>();

        // регистрация всех handlers в DI через Scrutor
        services.Scan(scan => scan.FromAssemblies([assembly])
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        return services;
    }
}