using asp.net_workshop_real_app_public.Data;
using asp.net_workshop_real_app_public.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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
        public async Task<IEnumerable<Apartment>> GetMyApartmentsAsync(string email)
        {
            var apartments = await _context.Apartments.Include(a=>a.person).Where(p=>p.email==email).ToListAsync();

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
        ///apartment id! צריך לקבל מהיוזר ואז אני אוכל למצוא את הדירה לעשות אובייקט חדש ולקשר!
        public async Task<bool> toggleLikedApartment(bool isLiked, string email, Guid apartmentId)
        {
            try
            {
                var apartment = await _context.Apartments.SingleOrDefaultAsync(a => a.apartmentId == apartmentId);
                var user = await _context.Users.SingleOrDefaultAsync(a => a.Email == email);

                if (isLiked)
                {
                    var isLikedApartmentAlready = await _context.likedApartments
                        .SingleOrDefaultAsync(a => a.person.Id == user.Id && a.apartment.apartmentId == apartmentId);

                    if (isLikedApartmentAlready != null)
                    {
                        // Apartment is already liked by the user
                        return false;
                    }

                    var likedApartment = new likedApartment
                    {
                        likedApartmentId = Guid.NewGuid(),
                        person = user,
                        apartment = apartment
                    };

                    await _context.likedApartments.AddAsync(likedApartment);
                }
                else
                {
                    var likedApartmentToRemove = await _context.likedApartments
                        .SingleOrDefaultAsync(a => a.person.Id == user.Id && a.apartment.apartmentId == apartmentId);

                    if (likedApartmentToRemove != null)
                    {
                        _context.likedApartments.Remove(likedApartmentToRemove);
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> addApartmentAsync(Apartment a,string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            a.apartmentId = Guid.NewGuid();
            a.person=user;
            await _context.Apartments.AddAsync(a);
            int isSuccessToAdd = await _context.SaveChangesAsync();

            return isSuccessToAdd > 0;
        }

        public async Task<IEnumerable<dynamic>> GetMyLikedApartmentsAsync(string email)
        {
            var likedApartments = await _context.likedApartments
                .Include(a => a.apartment)
                .Where(a => a.person.Email == email)
                .Include(a=>a.apartment).ToListAsync();

            return likedApartments;
        }

        public async Task<IEnumerable<Apartment>> SearchApartments(Dictionary<string, object> criteria)
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
