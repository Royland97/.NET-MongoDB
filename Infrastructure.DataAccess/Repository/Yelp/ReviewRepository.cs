using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository.Yelp
{
    public class ReviewRepository: GenericRepository<Review>, IReviewRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public ReviewRepository(ApplicationDbContext context):
            base(context)
        {
        }

        /// <summary>
        /// Obtiene un listado de Reviews dado el Id de un Business
        /// </summary>
        /// <param name="BusinessId"></param>
        /// <returns></returns>
        public IEnumerable<Review> GetReviewByBusinessId(string BusinessId)
        {
            var reviews = dbSet.Where(r => r.BusinessId == BusinessId && r.Stars > 4 && r.Useful == 1).AsNoTracking().AsEnumerable();
            return reviews.OrderByDescending(r => r.Date);
        }
    }
}
