using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.DataAccess.Repository.Yelp
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(ApplicationDbContext context) : 
            base(context) 
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los users
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<User> GetAllUsers(string search)
        {
            if (!string.IsNullOrEmpty(search))
                return dbSet.Where(u => u.ReviewCount > 2000 && u.Name.Contains(search)).OrderByDescending(u => u.ReviewCount).AsEnumerable();
            
            return dbSet.Where(u => u.ReviewCount > 2000).OrderByDescending(u => u.ReviewCount).AsNoTracking().AsEnumerable();
        }

        /// <summary>
        /// Obtiene un User por UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserByUserId(string userId)
        {
            //var filter = Builders<User>.Filter.Text(userId);
            return dbSet.Where(u => u.UserId == userId).FirstOrDefault();
        }

    }
}
