using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models{
    public class PlayList{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ? Id{ get; set;}

        public string username {get;set;} = null!;
        [BsonElement("items")]
        [JsonPropertyName("items")]
        public List<string> movieIds {get;set;} = null!;

    }
}