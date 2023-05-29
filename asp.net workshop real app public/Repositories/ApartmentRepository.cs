using asp.net_workshop_real_app_public.Data;
using asp.net_workshop_real_app_public.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

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



        public async Task<IEnumerable<Apartment>> GetAllRangeApartments(int page)
        {
            var pageNumber = page > 0 ? page : 1; 
            var skipCount = (pageNumber - 1) * 10;

            var apartments = await _context.Apartments
                .Skip(skipCount)
                .Take(10)
                .ToListAsync();

            return apartments;
        }
        public async Task<bool> toggleLikedApartment(likedApartment la, bool isLiked)
        {
            var existingLikedApartment = await _context.likedApartments.FirstOrDefaultAsync(a=>la.apartmentId==a.apartmentId&&la.email==a.email);

            if (existingLikedApartment == null)
            {
                await _context.likedApartments.AddAsync(la);
            }
            else
            {
                _context.likedApartments.Remove(existingLikedApartment);
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> addApartmentAsync(Apartment a)
        {
            a.apartmentId = Guid.NewGuid();
            await _context.Apartments.AddAsync(a);
            int isSuccessToAdd = await _context.SaveChangesAsync();

            return isSuccessToAdd > 0;
        }
        public async Task<List<Apartment>> SearchApartments(Dictionary<string, object> criteria)
        {
            var apartments=await _context.Apartments.ToListAsync();
            return  apartments.Where(apt =>
            {
                return criteria.Keys.All(key =>
                {
                    if (criteria[key] is bool)
                    {
                        return (bool)apt.GetType().GetProperty(key)?.GetValue(apt) == (bool)criteria[key];
                    }
                    if (criteria[key] is int)
                    {
                        return apt.GetType().GetProperty(key)?.GetValue(apt) != null &&
                               (int)apt.GetType().GetProperty(key)?.GetValue(apt) <= (int)criteria[key];
                    }
                    if (key != "apartmentId" && criteria[key] is string)
                    {
                        return apt.GetType().GetProperty(key)?.GetValue(apt)?.ToString()?.ToLower().Contains(criteria[key]?.ToString()?.ToLower()) ?? false;
                    }
                    return true;
                });
            }).ToList();
        }


    }
}
