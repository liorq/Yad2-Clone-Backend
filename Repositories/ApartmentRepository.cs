using asp.net_workshop_real_app_public.Data;
using asp.net_workshop_real_app_public.Models;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


        ///עובד על שני הפונקציות האלה 
        public async Task<IEnumerable<ApartmentSearchQuery>> getAllMySearches(string email)
        {
            var user= await _context.Users.FirstOrDefaultAsync(u=>u.Email == email);
            //var searches=await _context.searches.Where(s=>s.person == user).ToListAsync();
            //return searches;
            return null;
        }
        public async Task<IEnumerable<ApartmentSearchQuery>> removeMySearches(string email, ApartmentSearchQuery query)
        {

            //var searches = await _context.searches.Where(s => s.person == user).ToListAsync();
            return null;
        }
        public async Task<IEnumerable<Apartment>> GetAllapartmentsAsync()
        {
            var apartments = await _context.Apartments.ToListAsync();
            return apartments;
        }
        public async Task<IEnumerable<Apartment>> GetMyApartmentsAsync(string email)
        {
            var apartments = await _context.Apartments.Where(a=>a.person.Email==email).Include(a=>a.person).ToListAsync();

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

        public async Task<bool> removeApartmentAsync(Apartment a)
        {
            var apartment = await _context.Apartments.FirstOrDefaultAsync(apartment => apartment.apartmentId == a.apartmentId);
      
             _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();
            return true;
        
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

        public async Task<IEnumerable<Apartment>> SearchApartments(ApartmentSearchQuery apartment,string email)
        {
            ////add search to the data base
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Email==email);
            apartment.person = user;
            apartment.searchId= Guid.NewGuid();
            //await _context.searches.AddAsync(apartment);

            int counter = 0;
            var apartments = await _context.Apartments.ToListAsync();
            this.printObjectProperties(apartment);
            //foreach (var item in apartment.arrayOfTypeProperty)
            //{
            //    Console.WriteLine(item);
            //}
            Console.WriteLine(apartment.des);
            return apartments.Where(a =>
            {
                if (counter < 1)
                {
                counter++;

                //this.printObjectProperties(a);

                }
                int number = 0;
                if (a.des==null)
                {
                    return false;
                }
                foreach (var item in a.des.Split(' '))
                {
                    Console.WriteLine($"{number}: {item}");
                    number++;
                }
                //Console.WriteLine(a.des.Split(" "));
                //this.printObjectProperties(a);
                ////לשים סוגריים עגולים כדי למנוע שגיאות 
                //Console.WriteLine(a.city==apartment.city);
                /////////////////////////בדיקה האם הסוג נכס נמצא בבקשה
                ///שורה שבבדיקה
                bool condition =
                (a.des != null && apartment.des != null && a.des.Split(" ").Any(item => item.Trim().Equals(apartment.des.Trim()))) &&
                (apartment.arrayOfTypeProperty.Any(type => type == a.typeOfProperty )&&
                //a.city.Trim() == apartment.city.Trim()
                ( a.city == apartment.city|| a.city =="")

                && (( a.hasFurniture == apartment.hasFurniture) || !apartment.hasFurniture))&&
                    //a.hasKosherKitchen == apaptment.hasKosherKitchen &&
                    //a.hasCentralAirConditioning == apaptment.hasCentralAirConditioning &&
                   ( (a.hasElevator == apartment.hasElevator )|| !apartment.hasElevator) &&
                   (( a.hasAirConditioning == apartment.hasAirConditioning) || !apartment.hasAirConditioning )&&
                   ( (a.hasWindowBars == apartment.hasWindowBars) || !apartment.hasWindowBars )&&
                    ((a.isRenovated == apartment.isRenovated) || !apartment.isRenovated )&&
                    //a.isSmartHome == apaptment.isSmartHome &&
                    ((a.hasStorage == apartment.hasStorage) || !apartment.hasStorage )&&
                    //a.hasSolarHeater == apaptment.hasSolarHeater &&
                    ((a.hasAccessibilityForDisabled == apartment.hasAccessibilityForDisabled) || !apartment.hasAccessibilityForDisabled) &&
                    //a.isResidentialUnit == apaptment.isResidentialUnit &&
                   ( (a.totalSquareFootage >= apartment.minSqm) || apartment.minSqm==0) &&
                    ((a.totalSquareFootage <= apartment.maxSqm) || apartment.maxSqm == 0 )&&
                    (a.floor >= apartment.minFloor || apartment.minFloor == 0) &&
                    ( a.floor <= apartment.maxFloor || apartment.maxFloor == 0) &&
                    ((a.price >= apartment.minPrice) || apartment.minPrice == 0 )&&
                    ((a.price <= apartment.maxPrice) || apartment.maxPrice == 0) &&
                    ((a.roomNumber >= apartment.minRooms)|| apartment.minRooms==0 )&&
                    ((a.roomNumber <= apartment.maxRooms)|| apartment.maxRooms==0);

                if (condition)
                {
                    Console.WriteLine($"Apartment: {a.apartmentId} meets the criteria");
                }
                else
                    Console.WriteLine($"Apartment: {a.apartmentId}not meets the criteria");


                return condition;
            });
        }



    
        public void printObjectProperties(object obj)
    {
        var type = obj.GetType();
        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            var key = property.Name;
            var value = property.GetValue(obj);

            Console.WriteLine($"{key}: {value}");
        }
    }


    }
   
}
