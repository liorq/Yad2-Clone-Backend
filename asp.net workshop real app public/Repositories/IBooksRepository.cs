using asp.net_workshop_real_app_public.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace asp.net_workshop_real_app_public.Repositories
{
    public interface IBooksRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookById(int id);
        Task<int> AddBookAsync(NewBookModel newBookModel);
        Task<BookModel> UpdateBookAsync(int bookId, NewBookModel updatedModel);
        Task<BookModel> UpdateByPatch(int bookId, JsonPatchDocument updatedBook);
        Task<int> DeleteById(int bookId);
    }
}