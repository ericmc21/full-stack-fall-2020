using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Emailer.MongoDb
{
    public class MongoRepository<T> : IRepository<T> where T : Model
    {
        protected IMongoDatabase Database { get; }

        public MongoRepository(IMongoDatabase database)
        {
            Database = database;
        }

        protected string GetCollectionName() => typeof(T).Name + "s";

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await Database.GetCollection<T>(GetCollectionName())
                .Find(Builders<T>.Filter.Empty)
                .ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await Database.GetCollection<T>(GetCollectionName())
                .Find(x => x.id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(T item, CancellationToken cancellationToken = default)
        {
            await Database.GetCollection<T>(GetCollectionName())
                .InsertOneAsync(item, new InsertOneOptions(), cancellationToken);
        }

        public async Task UpdateAsync(T item, CancellationToken cancellationToken = default)
        {
            await Database.GetCollection<T>(GetCollectionName())
                .ReplaceOneAsync(
                    Builders<T>.Filter.Eq(x => x.id, item.id),
                    item, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(T item, CancellationToken cancellationToken = default)
        {
            await Database.GetCollection<T>(GetCollectionName()).DeleteOneAsync(
                Builders<T>.Filter.Eq(x => x.id, item.id), cancellationToken);
        }
    }
}