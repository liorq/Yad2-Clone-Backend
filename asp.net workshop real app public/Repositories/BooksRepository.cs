using asp.net_workshop_real_app_public.Data;
using asp.net_workshop_real_app_public.Models;
using Microsoft.EntityFrameworkCore;

namespace asp.net_workshop_real_app_public.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookstoreContext _context;
        public BooksRepository(BookstoreContext context)
        {
            _context = context;
        }

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var books = await _context.Books.Include(b => b.Author).ToListAsync();
            return books;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            /*var book = await _context.Books.FindAsync(id);*/
            var book = await _context.Books.Include(b => b.Author).Where(b => b.Id == id).FirstOrDefaultAsync();
            return book;
        }

        // תרגיל - תכתבו פונקציה (ותיישמו אותה) שמחזירה רשומה אחת בלבד לפי המזהה הייחודי
    }
}
