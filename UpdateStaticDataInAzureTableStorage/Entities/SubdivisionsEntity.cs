namespace UpdateStaticDataInAzureTableStorage.Entities
{
    public class SubdivisionsEntity : TableEntity
    {
        public string? SUCountry { get; set; }
        public string? SUCode { get; set; }
        public string? SUName { get; set; }
    }
}
