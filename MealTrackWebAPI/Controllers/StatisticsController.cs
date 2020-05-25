using MealTrackWebAPI.Models;
using MealTrackWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealTrackWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService statisticsService;

        public StatisticsController(StatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet("{userId}")]
        public ActionResult<Statistics> Get([FromServices]MealService mealService,string userId)
        {
            var meals = mealService.GetMeals(userId);
            Statistics averageMealNutrients = statisticsService.GetAverageMealNutrients(meals);
            return Ok(averageMealNutrients);
        }
    }
}