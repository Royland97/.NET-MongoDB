using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Web.ViewModels.Yelp
{
    [BindProperties]
    public class BusinessModel
    {
        public string Id { get; set; }
        public string? BusinessId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public decimal Stars { get; set; }
        public int ReviewCount { get; set; }
        public int IsOpen { get; set; }
    }
}
