using asp.net_workshop_real_app_public.Models;

namespace asp.net_workshop_real_app_public.Repositories
{
    public interface IApartmentRepository
    {

        public Task<IEnumerable<Apartment>> GetAllapartmentsAsync();
        public Task<bool>  addApartmentAsync(Apartment a);
    }
}