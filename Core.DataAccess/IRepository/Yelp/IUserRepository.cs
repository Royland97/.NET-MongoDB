using Core.Domain.Yelp;
using MongoDB.Bson;

namespace Core.DataAccess.IRepository.Yelp
{
    public interface IUserRepository: IGenericRepository<User>
    {
        IEnumerable<User> GetAllUsers();
    }
}
