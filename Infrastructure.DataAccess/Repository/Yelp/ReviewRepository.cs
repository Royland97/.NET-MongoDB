using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Infrastructure.DataAccess.Repository.Yelp
{
    public class ReviewRepository: GenericRepository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public ReviewRepository(ApplicationDbContext context):
            base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene un listado de Reviews dado el Id de un Business
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public IEnumerable<Review> GetReviewByBusinessId(string businessId)
        {
            return dbSet.Where(r => r.Stars > 4 && r.BusinessId == businessId).OrderByDescending(r => r.Date).AsNoTracking().AsEnumerable();
        }

        /// <summary>
        /// Obtiene todas las review dado un userId y la informacion de los business
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<ReviewBusiness> GetReviewBusinessByUserId(string userId)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("yelp");
            var reviews = database.GetCollection<Review>("review");

            var pipeline = new[]
                {
                    new BsonDocument("$match", new BsonDocument
                    {
                        { "$text", new BsonDocument { { "$search", userId } } }
                    }),

                    new BsonDocument("$lookup", new BsonDocument
                    {
                        { "from", "business" },
                        { "localField", "business_id" },
                        { "foreignField", "business_id" },
                        { "as", "businessInfo" }
                    }),

                    new BsonDocument("$unwind", "$businessInfo"),

                    new BsonDocument("$project", new BsonDocument
                    {
                        { "_id", 0 },
                        { "name", "$businessInfo.name" },
                        { "address", "$businessInfo.address" },
                        { "city", "$businessInfo.city" },
                        { "state", "$businessInfo.state" },
                        { "text", 1 },
                        { "date", 1 },
                    }),

                    new BsonDocument("$sort", new BsonDocument
                    {
                        { "date", 1 } 
                    })
                };
            
            var result = reviews.Aggregate<ReviewBusiness>(pipeline);

            return result.ToList();
        }   
    }
}
