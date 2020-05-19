using MealTrackWebAPI.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MealTrackWebAPI.Models.Meal
{
    public class Food
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public decimal Protein { get; set; }

        public decimal Fats { get; set; }

        public decimal Carbs { get; set; }

        public decimal Calories
        {
            get {
                return FoodHelper.CalculateCalories(this);
            }
        }

        public string ImagePath { get; set; }

        public string Image
        {
            get {
                return ImageHelper.ConvertImageToBase64String(string.Join(string.Empty,"food/",this.ImagePath));
            }
        }

    }
}