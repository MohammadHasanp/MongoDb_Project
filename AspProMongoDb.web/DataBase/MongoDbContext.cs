using AspProMongoDb.web.Model;
using MongoDB.Driver;

namespace AspProMongoDb.web.DataBase
{
    public class MongoDbContext(IMongoClient client, MongoSettings settings)
    {
        private readonly IClientSessionHandle _sessionHandle = client.StartSession();
        private readonly IMongoDatabase _database = client.GetDatabase(settings.DataBaseName);

        public IMongoDatabase GetDataBase()
        {
            return _database;
        }

        public IClientSessionHandle GetSession()
        {
            return _sessionHandle;
        }
        public void StartTransaction()
        {
            _sessionHandle.StartTransaction();
        }

        public void Commit()
        {
            _sessionHandle.CommitTransaction();
        }
    }
}
