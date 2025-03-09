using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository.Yelp
{
    public class ChekinRepository: GenericRepository<Chekin>, IChekinRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChekinRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public ChekinRepository(ApplicationDbContext context):
            base(context)
        {
        }

        /// <summary>
        /// Obtiene un chekin dado un businessId
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public async Task<Chekin> GetChekinByBusinessId(string businessId)
        {
            return await dbSet.Where(p => p.BusinessId == businessId).FirstOrDefaultAsync();
        }
    }
}
