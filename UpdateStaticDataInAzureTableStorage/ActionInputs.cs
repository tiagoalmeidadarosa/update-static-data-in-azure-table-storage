namespace UpdateStaticDataInAzureTableStorage;

public class ActionInputs
{
    [Option('t', "table_name",
    Required = true,
    HelpText = "The name of the table to insert into.")]
    public string TableName { get; set; } = null!;

    [Option('c', "connection_string",
    Required = true,
    HelpText = "The connection string for the storage account.")]
    public string ConnectionString { get; set; } = null!;
}
