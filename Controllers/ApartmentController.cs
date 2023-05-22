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
        [HttpGet("{page}")]

        public async Task<IActionResult> GetAllRangeApartments(int page)
        {
            var res = await _apartmentRepository.GetAllRangeApartments(page);
            if (res != null && res.Any())
            {
                return Ok(res);
            }
            return NotFound("No apartments to display.");
        }

        [HttpPost("")]
        public async Task<IActionResult> addApartment([FromBody] Apartment a)
        {
            Console.WriteLine("hi");
            bool isAddSucced = await _apartmentRepository.addApartmentAsync(a);
            if (isAddSucced)
            {
                return Ok();
            }
            return NotFound("Failed to add apartment");
        }
        [HttpPost("LikedApartment/{isLiked}")]

        public async Task<IActionResult> addLikedApartment([FromBody] likedApartment la,bool isLiked)
        {
            string email = User?.Identity?.Name;
            la.email = email;
            Console.WriteLine("email");
            Console.WriteLine(email);
            bool isAddSucced = await _apartmentRepository.toggleLikedApartment(la,isLiked);
 
            if (isAddSucced)
            {
                return Ok("add succes");
            }
            return NotFound("Failed to add apartment");
        }



    }
}