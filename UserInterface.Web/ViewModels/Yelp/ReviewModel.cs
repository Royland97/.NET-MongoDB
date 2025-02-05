namespace UserInterface.Web.ViewModels.Yelp
{
    public class ReviewModel
    {
        public string Id { get; set; }
        public string ReviewId { get; set; }
        public string UserId { get; set; }
        public string BusinessId { get; set; }
        public decimal Stars { get; set; }
        public int Useful { get; set; }
        public int Funny { get; set; }
        public int Cool { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
    }
}
