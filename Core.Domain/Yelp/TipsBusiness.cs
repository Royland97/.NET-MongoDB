using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Domain.Yelp
{
    public class TipsBusiness
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("state")]
        public string State { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }

        [BsonElement("cumplimient_count")]
        public int CumplimentCount { get; set; }
    }
}
