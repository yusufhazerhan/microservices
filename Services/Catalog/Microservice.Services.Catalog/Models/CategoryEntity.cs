using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Microservice.Services.Catalog.Models
{
    public class CategoryEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
