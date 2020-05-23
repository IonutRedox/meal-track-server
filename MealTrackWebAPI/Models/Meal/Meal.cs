using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MealTrackWebAPI.Models.Meal
{
    public class Meal
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public List<FoodPortion> FoodPortions { get; set; }

        public decimal Calories { get; set; }
    }
}