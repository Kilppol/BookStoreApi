using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStoreApi.Models;

public class Cobertura
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? IdCob { get; set; }

    [BsonElement("Name")]
    public string CoberturaName { get; set; } = null!;

    public string CoberturaDesc { get; set; } =null!;

}