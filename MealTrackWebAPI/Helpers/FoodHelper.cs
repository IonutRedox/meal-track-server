using MealTrackWebAPI.Models.Meal;

namespace MealTrackWebAPI.Helpers
{
    public enum Calories
    {
        Protein = 4,
        Fats = 9,
        Carbs = 4
    }

    public static class FoodHelper
    {
        public static decimal CalculateCalories(Food food)
        {
            return food.Protein * (decimal)Calories.Protein +
                   food.Fats * (decimal)Calories.Fats +
                   food.Carbs * (decimal)Calories.Carbs;
        }
    }
}