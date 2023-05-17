using asp.net_workshop_real_app_public.Models;
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

        public async Task<IActionResult> addApartment([FromBody] Apartment a)
        {
            bool isAddSucced = await _apartmentRepository.addApartmentAsync(a);
            if (isAddSucced)
            {
                return Ok();
            }
            return NotFound("Failed to add apartment");
        }
        [HttpPost("LikedApartment")]

        public async Task<IActionResult> addLikedApartment([FromBody] Apartment a)
        {
            bool isAddSucced = await _apartmentRepository.addApartmentAsync(a);
            if (isAddSucced)
            {
                return Ok();
            }
            return NotFound("Failed to add apartment");
        }



    }
}