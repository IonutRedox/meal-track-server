using MealTrackWebAPI.Models;
using MealTrackWebAPI.Models.Meal;
using System;
using System.Collections.Generic;

namespace MealTrackWebAPI.Services
{
    public class StatisticsService
    {
        public Statistics GetAverageMealNutrients(List<Meal> meals)
        {
            Statistics averageMealNutrients = new Statistics { Protein = 0,Fats = 0,Carbs = 0,UpperLimit = 0 };

            if(meals.Count == 0) {
                return averageMealNutrients;
            }

            decimal protein = 0;
            decimal fats = 0;
            decimal carbs = 0;
            int upperLimit = 0;
            foreach(Meal meal in meals) {
                foreach(FoodPortion foodPortion in meal.FoodPortions) {
                    protein += CalculateNutrientPerQuantity(foodPortion.Food.Protein,foodPortion.Quantity);
                    fats += CalculateNutrientPerQuantity(foodPortion.Food.Fats,foodPortion.Quantity);
                    carbs += CalculateNutrientPerQuantity(foodPortion.Food.Carbs,foodPortion.Quantity);
                }
            }

            protein /= meals.Count;
            fats /= meals.Count;
            carbs /= meals.Count;
            upperLimit = (int)Math.Max(Math.Max(protein,fats),carbs);
            upperLimit = (int)Math.Ceiling((decimal)upperLimit / 100) * 100;

            averageMealNutrients.Protein = (int)protein;
            averageMealNutrients.Fats = (int)fats;
            averageMealNutrients.Carbs = (int)carbs;
            averageMealNutrients.UpperLimit = upperLimit;

            return averageMealNutrients;
        }

        private static decimal CalculateNutrientPerQuantity(decimal valuePerHundread,decimal quantity)
        {
            decimal valuePerQuantity = (quantity * valuePerHundread) / 100;
            return valuePerQuantity;
        }
    }
}