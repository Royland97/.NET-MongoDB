using Core.Domain.Yelp;

namespace Core.DataAccess.IRepository.Yelp
{
    public interface IChekinRepository: IGenericRepository<Chekin>
    {
        /// <summary>
        /// Obtiene un chekin dado un businessId
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        Task<Chekin> GetChekinByBusinessId(string businessId);
    }
}
