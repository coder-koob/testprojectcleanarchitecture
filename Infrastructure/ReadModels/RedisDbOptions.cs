namespace Infrastructure.ReadModels;

public class RedisDbOptions
{
    public const string RedisDbSettings = "RedisDbSettings";

    public string ConnectionString { get; set; } = null!;
}