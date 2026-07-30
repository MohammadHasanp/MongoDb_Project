using AspProMongoDb.web.Comm;

namespace AspProMongoDb.web.Entities
{
    public class User:BaseEntity
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

}
