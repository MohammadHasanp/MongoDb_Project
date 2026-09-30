using AspProMongoDb.web.Comm;

namespace AspProMongoDb.web.Entities
{
    public class User:BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

}
