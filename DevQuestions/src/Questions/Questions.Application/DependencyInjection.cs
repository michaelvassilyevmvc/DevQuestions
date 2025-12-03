using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions;

namespace Questions.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddValidatorsFromAssembly(assembly);
        // services.AddScoped<ISearchProvider, SearchProvider>();

        // регистрация всех handlers в DI через Scrutor
        services.Scan(scan => scan.FromAssemblies([assembly])
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        
        // регистрация всех query в DI через Scrutor
        services.Scan(scan => scan.FromAssemblies([assembly])
            .AddClasses(classes => classes
                .AssignableToAny(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        return services;
    }
}