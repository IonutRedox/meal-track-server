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

        [HttpGet]
        public ActionResult<List<Meal>> Get()
        {
            return _mealService.Get();
        }

        [HttpPost]
        public ActionResult<Meal> Create([FromBody]Meal meal)
        {
            _mealService.Create(meal);
            return Ok(meal);
        }

        [HttpPut]
        public ActionResult<Meal> Update([FromBody]Meal mealIn)
        {
            var meal = _mealService.Get(mealIn.Id);

            if(meal == null) {
                return NotFound();
            }

            _mealService.Update(mealIn);

            return Ok(mealIn);
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult<Meal> Delete(string id)
        {
            var meal = _mealService.Get(id);

            if(meal == null) {
                return NotFound();
            }

            _mealService.Remove(meal);

            return Ok(meal);
        }
    }
}