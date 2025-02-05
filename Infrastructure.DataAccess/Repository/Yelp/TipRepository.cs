using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Infrastructure.DataAccess.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository.Yelp
{
    public class TipRepository: GenericRepository<Tip>, ITipRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TipRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public TipRepository(ApplicationDbContext context):
            base(context)
        {
        }
    }
}
