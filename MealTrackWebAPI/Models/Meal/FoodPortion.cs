namespace MealTrackWebAPI.Models.Meal
{
    public class FoodPortion
    {
        public Food Food { get; set; }

        public decimal Quantity { get; set; }
    }
}