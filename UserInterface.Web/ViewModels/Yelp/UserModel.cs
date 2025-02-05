using MongoDB.Bson;

namespace UserInterface.Web.ViewModels.Yelp
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public int ReviewCount { get; set; }
        public DateTime YelpingSince { get; set; }
        public int Useful { get; set; }
        public int Funny { get; set; }
        public int Cool { get; set; }
        public List<String> Elite { get; set; }
        //public List<String> Friends { get; set; }
        public int Fans { get; set; }
        public decimal AverageStars { get; set; }
        public int ComplimentHot { get; set; }
        public int ComplimentMore { get; set; }
        public int ComplimentProfile { get; set; }
        public int ComplimentCute { get; set; }
        public int ComplimentList { get; set; }
        public int ComplimentNote { get; set; }
        public int ComplimentPlain { get; set; }
        public int ComplimentCool { get; set; }
        public int ComplimentFunny { get; set; }
        public int ComplimentWriter { get; set; }
        public int ComplimentPhotos { get; set; }
    }
}
