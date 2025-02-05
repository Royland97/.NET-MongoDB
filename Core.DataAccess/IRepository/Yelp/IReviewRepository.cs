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
    }
}
