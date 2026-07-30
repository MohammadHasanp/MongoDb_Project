using AspProMongoDb.web.Comm;
using AspProMongoDb.web.DataBase;
using AspProMongoDb.web.Entities;

namespace AspProMongoDb.web.Services
{
    public class UserServices(MongoDbContext context) : BaseServices<User>(context), IUserServices;
}
