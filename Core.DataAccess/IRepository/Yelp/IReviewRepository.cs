using Core.Domain.Yelp;

namespace Core.DataAccess.IRepository.Yelp
{
    public interface IReviewRepository: IGenericRepository<Review>
    {
        /// <summary>
        /// Obtiene una Review dado el Id de un Business
        /// </summary>
        /// <param name="BusinessId"></param>
        /// <returns></returns>
        IEnumerable<Review> GetReviewByBusinessId(string BusinessId);

        //// <summary>
        /// Obtiene todas las review dado un userId y la informacion de los business
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<ReviewBusiness> GetReviewBusinessByUserId(string userId);
    }
}
