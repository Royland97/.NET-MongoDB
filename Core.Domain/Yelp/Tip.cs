using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace Core.Domain.Yelp
{
    [Collection("tips")]
    public class Tip: Entity
    {
        [BsonElement("user_id")]
        public string UserId { get; set; }

        [BsonElement("business_id")]
        public string BusinessId { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }

        [BsonElement("compliment_count")]
        public int ComplimentCount { get; set; }
    }
}
