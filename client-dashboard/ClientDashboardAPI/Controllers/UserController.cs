using System;
using System.Threading.Tasks;
using BCrypt.Net;
using ClientDashboardAPI.Data.Model;
using ClientDashboardAPI.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientDashboardAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel model)
        {
            if (await _userRepository.GetUserByEmail(model.Email) != null)
            {
                return BadRequest("User with this email already exists");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new User { Email = model.Email, PasswordHash = passwordHash };

            var userId = await _userRepository.CreateUser(user);

            return Ok(new { UserId = userId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserModel model)
        {
            var user = await _userRepository.GetUserByEmail(model.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(new { UserId = user.Id, user.Email });
        }
    }

    public class UserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}