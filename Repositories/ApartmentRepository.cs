using asp.net_workshop_real_app_public.Data;
using asp.net_workshop_real_app_public.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace asp.net_workshop_real_app_public.Repositories
{
    public class ApartmentRepository: IApartmentRepository
    {
  
          private readonly ApartementContext _context;

        public ApartmentRepository(ApartementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Apartment>> GetAllapartmentsAsync()
        {
            var apartments = await _context.Apartments.ToListAsync();
            return apartments;
        }
        public async Task<bool> addApartmentAsync(Apartment a)
        {
            await _context.Apartments.AddAsync(a);
            int isSuccessToAdd = await _context.SaveChangesAsync();

            return isSuccessToAdd > 0;
        } 


    }
}
