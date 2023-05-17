using asp.net_workshop_real_app_public.Data;
using asp.net_workshop_real_app_public.Models;
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

      

    }
}
