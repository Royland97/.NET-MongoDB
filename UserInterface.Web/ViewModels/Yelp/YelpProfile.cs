using Core.Domain.Yelp;

namespace UserInterface.Web.ViewModels.Yelp
{
    public class YelpProfile: AutoMapperProfile
    {
        public YelpProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            CreateMap<Business, BusinessModel>();
            CreateMap<BusinessModel, Business>()
                .ForMember(dst => dst.BusinessId, opt => opt.Ignore());
            CreateMap<Business, BusinessModelDetails>();

            CreateMap<Chekin, ChekinModel>();
            CreateMap<ChekinModel, Chekin>();

            CreateMap<Review, ReviewModel>();
            CreateMap<ReviewModel, Review>();

            CreateMap<Tip, TipModel>();
            CreateMap<TipModel, Tip>();
        }
    }
}
