using IHost host = Host.CreateDefaultBuilder(args).Build();

static TService Get<TService>(IHost host)
    where TService : notnull =>
    host.Services.GetRequiredService<TService>();

var parser = Default.ParseArguments<ActionInputs>(() => new(), args);
parser.WithNotParsed(
    errors =>
    {
        Get<ILoggerFactory>(host)
            .CreateLogger("UpdateStaticDataInAzureTableStorage.Program")
            .LogError(
                string.Join(
                    Environment.NewLine, errors.Select(error => error.ToString())));

        Environment.Exit(2);
    });

await parser.WithParsedAsync(options => StartAnalysisAsync(options, host));
await host.RunAsync();

static async Task StartAnalysisAsync(ActionInputs inputs, IHost host)
{
    var cloudStorageAccount = CloudStorageAccount.Parse(inputs.ConnectionString);

    var tableClient = cloudStorageAccount.CreateCloudTableClient();
    var table = tableClient.GetTableReference(inputs.TableName);
    await table.CreateIfNotExistsAsync();

    using (var reader = new StreamReader(inputs.CsvFilePath))
    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        MissingFieldFound = null,
        HeaderValidated = null,
        Delimiter = ",",
        DetectDelimiter = false,
        HasHeaderRecord = true,
        TrimOptions = TrimOptions.Trim,
    }))
    {
        switch (inputs.TableName)
        {
            case "locations":
                var locationRecords = csv.GetRecords<LocationsEntity>()
                    .GroupBy(r => r.PartitionKey)
                    .ToList();

                await ExecuteBatchAsync(table, locationRecords);
                break;

            case "payloads":
                var payloadRecords = csv.GetRecords<PayloadsEntity>()
                    .GroupBy(r => r.PartitionKey)
                    .ToList();

                await ExecuteBatchAsync(table, payloadRecords);
                break;

            case "subdivisions":
                var subdivisionsRecords = csv.GetRecords<SubdivisionsEntity>()
                    .GroupBy(r => r.PartitionKey)
                    .ToList();

                await ExecuteBatchAsync(table, subdivisionsRecords);
                break;

            default:
                break;
        }
    }

    await Task.CompletedTask;

    Environment.Exit(0);
}

static async Task ExecuteBatchAsync<T>(CloudTable table, List<IGrouping<string, T>> records) where T : TableEntity
{
    //Todo: Use the FarmerConnect.Azure.Storage.Table?
    //Microsoft.Azure.Cosmos.Table.StorageException: 'Server failed to authenticate the request. Make sure the value of Authorization header is formed correctly including the signature.'

    foreach (var record in records)
    {
        const int tableBatchMaxEntries = 100;

        var entities = record.ToList();
        var rowOffset = 0;

        while (rowOffset < entities.Count)
        {
            var rows = entities.Skip(rowOffset).Take(tableBatchMaxEntries).ToList();
            rowOffset += rows.Count;

            var batchOperation = new TableBatchOperation();

            foreach (var row in rows)
            {
                batchOperation.Add(TableOperation.InsertOrMerge(row));
            }

            await table.ExecuteBatchAsync(batchOperation);
        }
    }
}