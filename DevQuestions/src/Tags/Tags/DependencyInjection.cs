using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions;
using Tags.Database;

namespace Tags;

public static class DependencyInjection
{
    public static IServiceCollection AddTagsModule(this IServiceCollection services)
    {
        services.AddDbContext<TagsDbContext>();
        
        // регистрация всех handlers в DI через Scrutor
        services.Scan(scan => scan.FromAssemblies([TagsAssembly.Assembly])
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        
        // регистрация всех query в DI через Scrutor
        services.Scan(scan => scan.FromAssemblies([TagsAssembly.Assembly])
            .AddClasses(classes => classes
                .AssignableToAny(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        
        return services;
    } 
}