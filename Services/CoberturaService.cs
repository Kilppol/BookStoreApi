using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class CoberturaService
{
    private readonly IMongoCollection<Cobertura> _coberturaCollection;

    public CoberturaService(
        IOptions<CoberturaDatabaseSettings> coberturaDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            coberturaDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            coberturaDatabaseSettings.Value.DatabaseName);

        _coberturaCollection = mongoDatabase.GetCollection<Cobertura>(
            coberturaDatabaseSettings.Value.CoberturaCollectionName);
    }

    public async Task<List<Cobertura>> GetAsync() =>
        await _coberturaCollection.Find(_ => true).ToListAsync();

    public async Task<Cobertura?> GetAsync(string id) =>
        await _coberturaCollection.Find(x => x.IdCob == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Cobertura newCobertura) =>
        await _coberturaCollection.InsertOneAsync(newCobertura);

    public async Task UpdateAsync(string id, Cobertura updatedCobertura) =>
        await _coberturaCollection.ReplaceOneAsync(x => x.IdCob == id, updatedCobertura);

    public async Task RemoveAsync(string id) =>
        await _coberturaCollection.DeleteOneAsync(x => x.IdCob == id);
}