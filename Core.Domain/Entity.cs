using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Domain
{
    /// <summary>
    /// Base class for all entities.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Object Id.
        /// </summary>
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; protected set; }
    }
}
