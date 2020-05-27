using MealTrackWebAPI.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MealTrackWebAPI.Models.Authentication
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Email { get; set; }

        [BsonElement("Full name")]
        public string FullName { get; set; }

        public string Password { get; set; }

        public string ImagePath { get; set; }
    }
}