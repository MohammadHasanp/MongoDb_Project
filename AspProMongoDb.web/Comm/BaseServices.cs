using AspProMongoDb.web.DataBase;
using MongoDB.Driver;

namespace AspProMongoDb.web.Comm
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : BaseEntity
    {
        private readonly IMongoCollection<TEntity> _collection;
        private readonly MongoDbContext _dbContext;
        public BaseServices(MongoDbContext context)
        {
            _dbContext = context;
            var database = context.GetDataBase();
            _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public void Delete(Guid userid)
        {
            _dbContext.StartTransaction();
            _collection.DeleteOne(_dbContext.GetSession(), d => d.id == userid);
            _dbContext.Commit();
        }

        public List<TEntity> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public TEntity? GetById(Guid userId)
        {
            return _collection.Find(f => f.id == userId).FirstOrDefault();
        }

        public void Insert(TEntity entity)
        {
            _dbContext.StartTransaction();
            _collection.InsertOne(_dbContext.GetSession(), entity);
            _dbContext.Commit();
        }

        public void Update(TEntity entity)
        {
            _dbContext.StartTransaction();
            _collection.ReplaceOne(_dbContext.GetSession(), s => s.id == entity.id, entity);
            _dbContext.Commit();
        }
    }
}
