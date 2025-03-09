using Core.DataAccess.Filter.Yelp;
using Core.Domain.Yelp;

namespace Core.DataAccess.IRepository.Yelp
{
    public interface IBusinessRepository: IGenericRepository<Business>
    {
        /// <summary>
        /// Get all Business with filter options
        /// </summary>
        /// <returns></returns>
        IEnumerable<Business> GetAllBusinesses(string search);

        /// <summary>
        /// Obtiene los negocios de 5 estrellas con mas de 500 reviews
        /// </summary>
        /// <returns></returns>
        IEnumerable<Business> GetBetterBusiness();

        /// <summary>
        /// Obtiene un Business por BusinessId
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        Task<Business> GetBusinessByBusinessId(string businessId);

        /// <summary>
        /// Obtiene todos los estados 
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetAllStates();
    }
}
