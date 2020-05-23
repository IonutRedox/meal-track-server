using System;
using MealTrackWebAPI.Helpers;
using MealTrackWebAPI.Models.Authentication;
using MealTrackWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealTrackWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("sign-up")]
        public IActionResult SignUp([FromBody]User userIn)
        {
            try {
                _userService.SignUp(userIn);
                return Ok();
            } catch(AppException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("sign-in")]
        public IActionResult SignIn([FromBody]User userIn)
        {
            User user;
            try {
                user = _userService.SignIn(userIn.Email,userIn.Password);
                return Ok(new {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Token = new Guid().ToString()
                });
            } catch(AppException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}