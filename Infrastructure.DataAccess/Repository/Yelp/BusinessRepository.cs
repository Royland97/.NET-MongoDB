using Core.DataAccess.Filter.Yelp;
using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<Business> GetAllBusinesses(BusinessFilter businessFilter)
        {
            var businesses = dbSet.AsNoTracking().AsEnumerable().Take(10);

            if(businessFilter != null)
            {
                if (!string.IsNullOrEmpty(businessFilter.Name))
                    businesses = businesses.Where(b => b.Name.ToUpper().Equals(businessFilter.Name.ToUpper()));
            }

            businesses.OrderBy(c => c.Id);

            return businesses;
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
        public Task<Business> GetBusinessByBusinessId(string businessId)
        {
            return dbSet.Where(p => p.BusinessId == businessId).FirstOrDefaultAsync();
        }
    }
}
