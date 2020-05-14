using System;
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
            } catch(Exception ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("sign-in")]
        public IActionResult SignIn([FromBody]User userIn)
        {
            User user = _userService.SignIn(userIn.Email,userIn.Password);

            if(user == null) {
                return BadRequest(new { message = "Email or password is incorrect" });
            }

            return Ok(new {
                Email = user.Email,
                FullName = user.FullName,
                Token = new Guid().ToString()
            });
        }
    }
}