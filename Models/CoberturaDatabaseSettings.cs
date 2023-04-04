namespace BookStoreApi.Models;

public class CoberturaDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CoberturaCollectionName { get; set; } = null!;
}