using MealTrackWebAPI.Helpers;
using MealTrackWebAPI.Models;
using MealTrackWebAPI.Models.Meal;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealTrackWebAPI.Services
{
    public class FoodService
    {
        private readonly IMongoCollection<Food> _foods;

        public FoodService(IMealTrackDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _foods = database.GetCollection<Food>("Food");
        }

        public List<Food> Get() => _foods.Find(food => true).ToList();

        public Food Get(string id) => _foods.Find(food => food.Id == id).FirstOrDefault();
    }
}