using MealTrackWebAPI.Models.Meal;
using MealTrackWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace MealTrackWebAPI.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly FoodService _foodService;

        public FoodController(FoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet]
        public ActionResult<List<Food>> Get() => _foodService.GetFoods();

        [HttpGet("{id:length(24)}",Name = "GetFood")]
        public ActionResult<Food> Get(string id)
        {
            var food = _foodService.Get(id);

            if(food == null) {
                return NotFound();
            }

            return food;
        }
    }
}