using Core.Domain.Yelp;

namespace Core.DataAccess.IRepository.Yelp
{
    public interface IUserRepository: IGenericRepository<User>
    {
        IEnumerable<User> GetAllUsers(string search);

        /// <summary>
        /// Obtiene un User por UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetUserByUserId(string userId);
    }
}
