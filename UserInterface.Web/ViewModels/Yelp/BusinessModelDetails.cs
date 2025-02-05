using Core.Domain.Yelp;

namespace UserInterface.Web.ViewModels.Yelp
{
    public class BusinessModelDetails
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal Stars { get; set; }
        public int ReviewCount { get; set; }
        public int IsOpen { get; set; }
        public List<String> Categories { get; set; }
        public BusinessHours? Hours { get; set; }
    }
}
