using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace Core.Domain.Yelp
{
    [Collection("user")]
    public class User: Entity
    {
        [BsonElement("user_id")]
        public string UserId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("review_count")]
        public int ReviewCount { get; set; }
        
        [BsonElement("yelping_since")]
        public string YelpingSince { get; set; }

        [BsonElement("useful")]
        public int Useful {  get; set; }

        [BsonElement("funny")]
        public int Funny { get; set; }

        [BsonElement("cool")]
        public int Cool {  get; set; }

        [BsonElement("elite")]
        public List<String> Elite {  get; set; }

        //[BsonElement("friends")]
        //public List<String> Friends { get; set; }

        [BsonElement("fans")]
        public int Fans { get; set; }

        [BsonElement("average_stars")]
        public decimal AverageStars {  get; set; }

        [BsonElement("compliment_hot")]
        public int ComplimentHot { get; set; }

        [BsonElement("compliment_more")]
        public int ComplimentMore { get; set; }

        [BsonElement("compliment_profile")]
        public int ComplimentProfile { get; set; }

        [BsonElement("compliment_cute")]
        public int ComplimentCute { get; set; }

        [BsonElement("compliment_list")]
        public int ComplimentList { get; set; }

        [BsonElement("compliment_note")]
        public int ComplimentNote { get; set; }

        [BsonElement("compliment_plain")]
        public int ComplimentPlain { get; set; }

        [BsonElement("compliment_cool")]
        public int ComplimentCool { get; set; }

        [BsonElement("compliment_funny")]
        public int ComplimentFunny { get; set; }

        [BsonElement("compliment_writer")]
        public int ComplimentWriter { get; set; }

        [BsonElement("compliment_photos")]
        public int ComplimentPhotos { get; set; }
    }
}
