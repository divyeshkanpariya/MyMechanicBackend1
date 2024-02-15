using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMechanic.Repositories.Interface;
using MyMechanic.ViewModels;

namespace MyMechanicBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GarageController : ControllerBase
    {
        private readonly IGarageRepo _GarageRepo;

        public GarageController(IGarageRepo GarageRepo)
        {
            _GarageRepo = GarageRepo;
        }
        [HttpGet("GetGarageDetails")]
        public IActionResult GetGarageDetails(string MechanicId)
        {
            
            return Ok(_GarageRepo.GetGarageDetails(Convert.ToInt64(MechanicId)));
        }

        [HttpPost("AddEditGarage")]
        public IActionResult AddEditGarage(NewGarageModel data)
        {
            
            _GarageRepo.AddEditGarage(data);
            return Ok();
        }

        [HttpDelete("DeleteGarage")]
        public IActionResult DeleteGarage(long GarageId)
        {
            _GarageRepo.DeleteGarage(Convert.ToInt64(GarageId));
            return Ok();
        }

        [HttpPost("AddGaragePhotos")]
        public IActionResult AddGaragePhotos([FromForm] NewGaragePhotosModel model, long GarageId, long UserId)
        {
            if (model?.Files == null || model.Files.Count == 0)
            {
                return BadRequest("No files were uploaded.");
            }

            // Process each uploaded file
            _GarageRepo.UploadGaragePhotos(model,GarageId,UserId);

            return Ok();
        }

        
        [HttpGet("GetGaragePhotos")]
        public IActionResult GetGaragePhotos(long GarageId, long UserId)
        {
            
            return Ok(_GarageRepo.GetGaragePhotos(GarageId, UserId));
        }

        [HttpDelete("DeleteGaragePhoto")]
        public IActionResult DeleteGaragePhoto(long GarageId, long GaragePhotoId)
        {
            
            String status = _GarageRepo.DeleteGaragePhoto(GarageId, GaragePhotoId);
            if (status == "Image Does not Exiest") return BadRequest(status);
            else return Ok();
        }

        [HttpGet("GetGarageDataForCustomer")]
        public IActionResult GetGarageDataForCustomer()
        {
            var x = _GarageRepo.GetGarageDataForCustomer();
            return Ok(x);
        }
        [HttpGet("GetGarageDataByGarageId")]
        public IActionResult GetGarageDataByGarageId(long GarageId)
        {
            return Ok(_GarageRepo.GetGarageDataByGarageId(GarageId));
        }
    }
}
