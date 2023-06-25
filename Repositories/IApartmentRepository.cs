using asp.net_workshop_real_app_public.Models;

namespace asp.net_workshop_real_app_public.Repositories
{
    public interface IApartmentRepository
    {

        public Task<IEnumerable<Apartment>> GetAllapartmentsAsync();
        public Task<bool>  addApartmentAsync(Apartment a, string email);
        public Task<IEnumerable<Apartment>> GetAllRangeApartments(int page);
        public Task<bool> toggleLikedApartment(bool isLiked, string email, Guid apartmentId);
        public  Task<IEnumerable<Apartment>> SearchApartments(ApartmentSearchQuery criteria, string email);

        public Task<IEnumerable<Apartment>> GetMyApartmentsAsync(string email);
        public Task<bool> addCommentToLikedApartment(Guid likedApartmentId, string comment);
        public  Task<bool> removeApartmentAsync(Apartment a);
        public Task<IEnumerable<dynamic>> GetMyLikedApartmentsAsync(string email);
        public void printObjectProperties(object obj);
        public Task<IEnumerable<ApartmentSearchQuery>> getAllMySearches(string email);

        public Task<IEnumerable<Pic>> getImageString(Guid apartmentId);
    }
}