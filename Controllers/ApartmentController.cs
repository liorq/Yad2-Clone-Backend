using asp.net_workshop_real_app_public.Models;
using asp.net_workshop_real_app_public.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace asp.net_workshop_real_app_public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountRepository _accountRepository;

        public ApartmentController(IAccountRepository accountRepository,IApartmentRepository apartmentRepository, IHttpContextAccessor httpContextAccessor)
        {
            _apartmentRepository = apartmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _accountRepository = accountRepository;

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

        [HttpGet("myApartments")]

        public async Task<IActionResult> GetMyApartments()
        {

            string email =  _accountRepository.getUserNameByToken();
                var res = await _apartmentRepository.GetMyApartmentsAsync(email);
            if (res != null && res.Any())
            {
                return Ok(res);
            }
            return NotFound("No apartments to display.");
        }

        [HttpDelete("removeApartments")]
        public async Task<IActionResult> removeApartment([FromBody] Apartment a)
        {
            bool isAddSucced = await _apartmentRepository.removeApartmentAsync(a);
            if (isAddSucced)
            {
                return Ok();
            }
            return NotFound("Failed to remove apartment");
        }
        [HttpPost("myApartments/likedApartments")]
        public async Task<IActionResult> GetMyLikedApartments()
        {
            string email = _accountRepository.getUserNameByToken();

            var res = await _apartmentRepository.GetMyLikedApartmentsAsync(email);
 
            if (res != null && res.Any())
            {
                return Ok(res);
            }
            return NotFound("No apartments liked to display.");
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
            string email = _accountRepository.getUserNameByToken();

            bool isAddSucced = await _apartmentRepository.addApartmentAsync(a, email);
            if (isAddSucced)
            {
                return Ok();
            }
            return NotFound("Failed to add apartment");
        }
        [HttpPost("LikedApartment/{apartmentId}/{isLiked}")]

        public async Task<IActionResult> addLikedApartment(Guid apartmentId, bool isLiked)
        {
            string email = _accountRepository.getUserNameByToken();

            bool isAddSucced = await _apartmentRepository.toggleLikedApartment(isLiked, email, apartmentId);
 
            if (isAddSucced)
            {
                return Ok("add succes");
            }
            return NotFound("Failed to add apartment");
        }



        [HttpPost("SearchApartments")]
        public async Task<IActionResult> SearchApartments([FromBody] ApartmentSearchQuery apartmentSearchQuery)
        {

            var result = await _apartmentRepository.SearchApartments(apartmentSearchQuery);

            if (result != null && result.Any())
            {
                return Ok(result);
            }
            return NotFound("Failed to get apartments");
        }

        //[HttpGet("token")]
        //public string? getUserNameByToken()
        //{
        //    var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        //    if (token == "")
        //        return "you dont have token send";

        //    var handler = new JwtSecurityTokenHandler();
        //    var decodedToken = handler.ReadJwtToken(token);
        //    return decodedToken?.Claims?.ToArray()[0]?.Value;
        //}


    }
}