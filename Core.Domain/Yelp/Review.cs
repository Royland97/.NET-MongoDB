using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace Core.Domain.Yelp
{
    [Collection("review")]
    public class Review: Entity
    {
        [BsonElement("review_id")]
        public string ReviewId { get; set; }

        [BsonElement("user_id")]
        public string UserId { get; set; }

        [BsonElement("business_id")]
        public string BusinessId { get; set; }

        [BsonElement("stars")]
        public decimal Stars { get; set; }

        [BsonElement("useful")]
        public int Useful { get; set; }

        [BsonElement("funny")]
        public int Funny { get; set; }

        [BsonElement("cool")]
        public int Cool {  get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }
    }
}
