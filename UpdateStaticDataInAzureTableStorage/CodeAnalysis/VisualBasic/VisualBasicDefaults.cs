using Microsoft.CodeAnalysis.VisualBasic;

namespace UpdateStaticDataInAzureTableStorage.CodeAnalysis.VisualBasic;

static class VisualBasicDefaults
{
    internal static VisualBasicCompilationOptions CompilationOptions { get; } =
        new(OutputKind.ConsoleApplication, concurrentBuild: false);

    internal static VisualBasicCommandLineParser CommandLineParser { get; } =
        VisualBasicCommandLineParser.Default;
}