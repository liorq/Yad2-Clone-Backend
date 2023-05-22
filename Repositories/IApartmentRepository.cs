using asp.net_workshop_real_app_public.Models;

namespace asp.net_workshop_real_app_public.Repositories
{
    public interface IApartmentRepository
    {

        public Task<IEnumerable<Apartment>> GetAllapartmentsAsync();
        public Task<bool>  addApartmentAsync(Apartment a);
        public Task<IEnumerable<Apartment>> GetAllRangeApartments(int page);
        public Task<bool> toggleLikedApartment(likedApartment la, bool isLiked);
        
    }
}