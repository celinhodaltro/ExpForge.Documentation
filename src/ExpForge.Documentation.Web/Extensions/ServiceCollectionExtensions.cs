using ExpForge.Documentation.Application.Interfaces;
using ExpForge.Documentation.Application.Services;
using ExpForge.Documentation.Domain.Interfaces;
using ExpForge.Documentation.Infrastructure.Repositories;
using ExpForge.Documentation.Infrastructure.Services;

namespace ExpForge.Documentation.Web.Extensions;

/// <summary>
/// Extensões para configuração de serviços
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configura os serviços da aplicação
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Repositórios
        services.AddSingleton<IWidgetRepository, InMemoryWidgetRepository>();
        services.AddSingleton<ITemplateRepository, InMemoryTemplateRepository>();
        
        // Serviços
        services.AddScoped<IWidgetService, WidgetService>();
        services.AddScoped<IExpForgeService, ExpForgeService>();
        
        return services;
    }
}
