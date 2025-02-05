using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace Core.Domain.Yelp
{
    [Collection("chekin")]
    public class Chekin: Entity
    {
        [BsonElement("business_id")]
        public string BusinessId { get; set; }

        [BsonElement("date")]
        public List<String> Date { get; set; }
    }
}
