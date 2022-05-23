namespace UpdateStaticDataInAzureTableStorage;

public class ActionInputs
{
    [Option('c', "connection_string",
    Required = true,
    HelpText = "The connection string for the storage account.")]
    public string ConnectionString { get; set; } = null!;

    [Option('p', "csv_file_paths",
    Required = true,
    HelpText = "The csv file paths for import.")]
    public string CsvFilePaths { get; set; } = null!;
}
