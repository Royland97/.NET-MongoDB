using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Infrastructure.DataAccess.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

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

        /// <summary>
        /// Get all Tips and Business Data related from an user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<TipsBusiness> GetTipsBusinessByUserId(string userId)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("yelp");
            var reviews = database.GetCollection<Review>("tips");

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
                        { "cumpliment_count", 1 },
                    }),

                    new BsonDocument("$sort", new BsonDocument
                    {
                        { "date", 1 }
                    })
                };

            var result = reviews.Aggregate<TipsBusiness>(pipeline);

            return result.ToList();
        }

    }
}
