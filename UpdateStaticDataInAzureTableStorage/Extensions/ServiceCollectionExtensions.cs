namespace UpdateStaticDataInAzureTableStorage.Extensions;

static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddGitHubActionServices(
        this IServiceCollection services) =>
        services.AddSingleton<ProjectMetricDataAnalyzer>()
                .AddDotNetCodeAnalysisServices();

    public static IServiceCollection AddDotNetCodeAnalysisServices(
        this IServiceCollection services)
    {
        MSBuildLocator.RegisterDefaults();

        return services.AddSingleton<ProjectLoader>()
            .AddSingleton<ProjectWorkspace>();
    }
}
