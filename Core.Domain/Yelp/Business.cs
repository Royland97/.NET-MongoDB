using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace Core.Domain.Yelp
{
    [Collection("business")]
    public class Business: Entity
    {
        [BsonElement("business_id")]
        public string BusinessId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("state")]
        public string State { get; set; }

        [BsonElement("postal_code")]
        public string PostalCode { get; set; }
        
        [BsonElement("latitude")]
        public double Latitude { get; set; }

        [BsonElement("longitude")]
        public double Longitude { get; set; }

        [BsonElement("stars")]
        public decimal Stars { get; set; }

        [BsonElement("review_count")]
        public int ReviewCount { get; set; }
        
        [BsonElement("is_open")]
        public int IsOpen { get; set; }
        
        [BsonElement("categories")]
        public List<String> Categories { get; set; }

        [BsonElement("hours")]
        public BusinessHours? Hours { get; set; }
    }
}
