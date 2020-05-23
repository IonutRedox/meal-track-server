using MealTrackWebAPI.Models;
using MealTrackWebAPI.Models.Meal;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace MealTrackWebAPI.Services
{
    public class MealService
    {
        private readonly IMongoCollection<Meal> _meals;

        public MealService(IMealTrackDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _meals = database.GetCollection<Meal>("Meal");
        }

        public List<Meal> GetMeals(string userId)
        {
            return _meals.Find(meal => meal.UserId == userId).ToList();
        }

        public Meal GetMeal(string id)
        {
            return _meals.Find<Meal>(meal => meal.Id == id).FirstOrDefault();
        }

        public Meal Create(Meal meal)
        {
            _meals.InsertOne(meal);
            return meal;
        }

        public Meal Update(Meal mealIn)
        {
            _meals.ReplaceOne(meal => meal.Id == mealIn.Id,mealIn);
            return mealIn;
        }

        public Meal Remove(Meal mealIn)
        {
            _meals.DeleteOne(meal => meal.Id == mealIn.Id);
            return mealIn;
        }
    }
}