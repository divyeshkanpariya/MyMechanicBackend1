using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMechanic.Repositories.Interface;

namespace MyMechanicBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ILocationRepo _LocationRepo;

        public CommonController(ILocationRepo locationRepo)
        {
            _LocationRepo = locationRepo;
        }
        [Authorize]
        [HttpGet("GetAllStates")]
        public IActionResult GetStates()
        {
            return Ok(_LocationRepo.GetAllStates());
        }
        [Authorize]
        [HttpGet("GetAllCities")]
        public IActionResult GetCities()
        {
            return Ok(_LocationRepo.GetAllCities());
        }
        [Authorize]
        [HttpGet("GetAllServiceTypes")]
        public IActionResult GetAllServiceTypes() 
        {
            return Ok(_LocationRepo.GetAllServiceTypes());
        }
    }
}
