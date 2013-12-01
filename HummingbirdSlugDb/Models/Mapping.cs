using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;

namespace HummingbirdSlugDb.Models
{
    public class Mapping
    {
        [JsonIgnore]
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        [BsonElement]
        public int TvDBId { get; set; }
        [BsonElement]
        public string OverrideSlug { get; set; }
        [BsonElement]
        public Dictionary<int, string> SeasonOverrides { get; set; }
        [BsonElement]
        public Dictionary<int, string> SpecialOverrides { get; set; } 
    }
}