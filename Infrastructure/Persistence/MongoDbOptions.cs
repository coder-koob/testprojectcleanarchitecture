namespace Infrastructure.Persistence;

public class MongoDbOptions
{
    public const string MongoDbSettings = "MongoDbSettings";

    public string ConnectionString { get; set; } = null!;
    public string Database { get; set; } = null!;
    public string Collection { get; set; } = null!;
}