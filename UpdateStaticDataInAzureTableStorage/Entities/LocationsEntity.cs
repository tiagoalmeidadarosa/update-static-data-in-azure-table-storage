namespace UpdateStaticDataInAzureTableStorage.Entities
{
    public class LocationsEntity : TableEntity
    {
        public string? Code { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public string? CountryNumericCode { get; set; }
        public string? ExportLocation { get; set; }
        public string? Function { get; set; }
        public string? ImportLocation { get; set; }
        public bool IsAirport { get; set; }
        public bool IsPort { get; set; }
        public bool IsServiceProvider { get; set; }
        public string? LocationCoordinates { get; set; }
        public string? Name { get; set; }
        public string? NeutralLocation { get; set; }
        public string? Subdivision { get; set; }
        public string? PortCoordinates { get; set; }
        public string? City { get; set; }
        public string? ServiceProviderType { get; set; }
        public string? Website { get; set; }
    }
}
