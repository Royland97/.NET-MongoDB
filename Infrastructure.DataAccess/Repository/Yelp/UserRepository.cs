using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository.Yelp
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(ApplicationDbContext context): 
            base(context) 
        {
        }

        public IEnumerable<User> GetAllUsers()
        {
            return dbSet.Where(u => u.ReviewCount > 2000).OrderByDescending(u => u.ReviewCount).AsNoTracking().AsEnumerable();
        }
    }
}
