using MealTrackWebAPI.Models.Meal;
using MealTrackWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MealTrackWebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MealsController : ControllerBase
    {
        private readonly MealService _mealService;

        public MealsController(MealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet("{userId:length(24)}")]
        public ActionResult<List<Meal>> Get(string userId)
        {
            return _mealService.GetMeals(userId);
        }

        [HttpPost("{userId:length(24)}")]
        public ActionResult<Meal> Create([FromBody]Meal meal,string userId)
        {
            meal.UserId = userId;
            _mealService.Create(meal);
            return Ok(meal);
        }

        [HttpPut("{userId:length(24)}")]
        public ActionResult<Meal> Update([FromBody]Meal mealIn,string userId)
        {
            mealIn.UserId = userId;
            var meal = _mealService.GetMeal(mealIn.Id);

            if(meal == null) {
                return NotFound();
            }

            _mealService.Update(mealIn);

            return Ok(mealIn);
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult<Meal> Delete(string id)
        {
            var meal = _mealService.GetMeal(id);

            if(meal == null) {
                return NotFound();
            }

            _mealService.Remove(meal);

            return Ok(meal);
        }
    }
}