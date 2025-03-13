using Core.Domain.Yelp;

namespace Core.DataAccess.IRepository.Yelp
{
    public interface ITipRepository: IGenericRepository<Tip>
    {
        /// <summary>
        /// Get all Tips and Business Data related from an user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<TipsBusiness> GetTipsBusinessByUserId(string userId);
    }
}
