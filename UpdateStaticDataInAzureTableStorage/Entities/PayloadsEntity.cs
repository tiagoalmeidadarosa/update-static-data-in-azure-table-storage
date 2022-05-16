namespace UpdateStaticDataInAzureTableStorage.Entities
{
    public class PayloadsEntity : TableEntity
    {
        public string? CertificateType { get; set; }
        public string? CountryName { get; set; }
        public string? Key { get; set; }
        public string? ProgramOwner { get; set; }
        public string? Title { get; set; }
        public string? Value { get; set; }
    }
}
