using System;
using System.IO;
using MealTrackWebAPI.Helpers;
using MealTrackWebAPI.Models.Authentication;
using MealTrackWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MealTrackWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        const string USER_PROFILE_IMAGE_URL = "https://localhost:44316/content/images/users/";

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("sign-up")]
        public IActionResult SignUp([FromBody]User userIn)
        {
            try {
                userIn.ImagePath = "unknown_profile.png";
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
                    Image = Path.Combine(USER_PROFILE_IMAGE_URL,user.ImagePath),
                    Token = new Guid().ToString()
                });
            } catch(AppException ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("update/{userId:length(24)}")]
        public IActionResult Update([ModelBinder(BinderType = typeof(JsonModelBinder))] User user,IFormFile file,string userId)
        {
            string fileName;
            if(file != null) {
                var folderName = Path.Combine("Content","images","users");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),folderName);
                fileName = $"{userId}_profile.png";

                using(var fileStream = new FileStream(Path.Combine(filePath,fileName),FileMode.Create)) {
                    file.CopyTo(fileStream);
                }

                user.ImagePath = fileName;
            }

            var result = _userService.Update(userId,user);

            return Ok(new {
                Id = result.Id,
                Email = result.Email,
                FullName = result.FullName,
                Image = Path.Combine(USER_PROFILE_IMAGE_URL,user.ImagePath)
            });
        }
    }
}