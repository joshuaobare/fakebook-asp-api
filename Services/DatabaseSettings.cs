namespace fakebook_asp_api.Services
{
    public class DatabaseSettings
    {
        public string MongoDBConnection { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}
