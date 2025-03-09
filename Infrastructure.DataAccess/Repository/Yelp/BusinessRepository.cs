using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Infrastructure.DataAccess.Repository.Yelp
{
    public class BusinessRepository: GenericRepository<Business>, IBusinessRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public BusinessRepository(ApplicationDbContext context):
            base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all Business with filter options
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Business> GetAllBusinesses(string search)
        {
            if (!string.IsNullOrEmpty(search))
                return dbSet.Where(p => p.Stars > 4 && p.ReviewCount > 500 && p.Name.Contains(search)).OrderByDescending(p => p.ReviewCount).AsNoTracking().AsEnumerable();

            return dbSet.Where(p => p.Stars > 4 && p.ReviewCount > 500).OrderByDescending(p => p.ReviewCount).AsNoTracking().AsEnumerable();
        }

        /// <summary>
        /// Obtiene los negocios de 5 estrellas con mas de 500 reviews
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Business> GetBetterBusiness()
        {
            return dbSet.Where(p => p.Stars > 4 && p.ReviewCount > 500).OrderByDescending(p => p.ReviewCount).AsNoTracking().AsEnumerable();
        }

        /// <summary>
        /// Obtiene un Business por BusinessId
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public async Task<Business> GetBusinessByBusinessId(string businessId)
        {
            return await dbSet.Where(p => p.BusinessId == businessId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Obtiene todos los estados 
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetAllStates()
        {
            IQueryable<string> stateQuery = from m in _context.Businesses
                                            orderby m.State
                                            select m.State;

            return await stateQuery.Distinct().ToListAsync();
        }

    }
}
