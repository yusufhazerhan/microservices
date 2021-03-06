using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Microservice.Services.Catalog.Models
{
    public class CourseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price{ get; set; }
        public string Description{ get; set; }
        public string UserId{ get; set; }
        public string Picture{ get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedTime{ get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId{ get; set; }
        public FeatureEntity Feature{ get; set; }

        [BsonIgnore]
        public CategoryEntity Category { get; set; }
    }
}
