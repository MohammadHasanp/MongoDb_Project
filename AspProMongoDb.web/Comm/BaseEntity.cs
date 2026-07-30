using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AspProMongoDb.web.Comm
{
    public class BaseEntity
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid id { get; set; }
    }
}
