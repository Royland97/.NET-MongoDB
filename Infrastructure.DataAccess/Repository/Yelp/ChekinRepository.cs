using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Infrastructure.DataAccess.EntityFrameworkCore;

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
    }
}
