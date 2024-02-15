using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMechanic.Entities.Models;
using MyMechanic.Repositories.Interface;
using MyMechanic.ViewModels;

namespace MyMechanicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;

        public AuthController(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel data)
        {
            return Ok(_authRepo.VarifyUser(data));  
        }
        [HttpPost("register")]
        public IActionResult Register(RegistrationModel Data)
        {
            return Ok(_authRepo.RegisterNewUser(Data));
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetInfo()
        {
            return Ok();
        }
    }
}
