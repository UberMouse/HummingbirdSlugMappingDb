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
        [BsonIgnoreIfDefault]
        public Guid Id { get; set; }

        [BsonElement]
        public int TvDBId { get; set; }
        [BsonElement]
        public string OverrideSlug { get; set; }
        [BsonElement]
        public Dictionary<int, string> SeasonOverrides { get; set; }
        [BsonElement]
        public Dictionary<string, string> SpecialOverrides { get; set; }

        protected bool Equals(Mapping other)
        {
            return TvDBId == other.TvDBId && string.Equals(OverrideSlug, other.OverrideSlug) && Equals(SeasonOverrides, other.SeasonOverrides) && Equals(SpecialOverrides, other.SpecialOverrides);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Mapping)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = TvDBId;
                hashCode = (hashCode * 397) ^ (OverrideSlug != null ? OverrideSlug.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SeasonOverrides != null ? SeasonOverrides.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SpecialOverrides != null ? SpecialOverrides.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}