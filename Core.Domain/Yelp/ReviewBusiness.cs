using MongoDB.Bson.Serialization.Attributes;

namespace Core.Domain.Yelp
{
    public class ReviewBusiness
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("state")]
        public string State { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }
    }
}
