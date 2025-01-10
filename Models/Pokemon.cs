// using MongoDB.Bson;
// using MongoDB.Bson.Serialization.Attributes;

namespace Pokemon.Models
{
    public class Pokemons
    {
        // [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        // public string ObjectId { get; set; }

        // [BsonElement("Id")]
        public int Id { get; set; }

        // [BsonElement("Name")]
        public string? Name { get; set; }

        // [BsonElement("Type")]
        public string? Type { get; set; }

        // [BsonElement("Ability")]
        public string? Ability { get; set; }

        // [BsonElement("Level")]
        public int Level { get; set; }
    }
}
