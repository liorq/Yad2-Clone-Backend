using asp.net_workshop_real_app_public.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp.net_workshop_real_app_public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentController(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        [HttpGet("")]

        public async Task<IActionResult> GetAllapartments()
        {
            var res = await _apartmentRepository.GetAllapartmentsAsync();
            if (res != null && res.Any())
            {
                return Ok(res);
            }
            return NotFound("No apartments to display.");
        }


        [HttpPost("")]

        public async Task<IActionResult> addApartment()
        {
            var res = await _apartmentRepository.addApartmentAsync();
            if (res != null && res.Any())
            {
                return Ok(res);
            }
            return NotFound("No apartments to display.");
        }




        [HttpGet("")]

        public async Task<IActionResult> GetLikedapartments()
        {
            var res = await _apartmentRepository.GetAllapartmentsAsync();
            if (res != null && res.Any())
            {
                return Ok(res);
            }
            return NotFound("No apartments to display.");
        }
    }
}