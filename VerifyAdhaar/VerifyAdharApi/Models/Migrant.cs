using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace VerifyAdharApi.Models
{
    public class Migrant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        public string AadharNumber { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }

        public List<string> Skill { get; set; }
    }
}
