using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IAuthorsService
    {
        Task<Authors> FindAuthorByIdAsync(int id);
        Task AddAuthorAsync(Authors author);
        Task UpdateAuthorAsync(Authors author);
        Task DeleteAuthorAsync(int id);
        Task DeleteMultipleAuthorsAsync(List<int> authorIds);
        Task<(IEnumerable<Authors> Authors, int TotalCount)> GetAllAuthorsAsync(int pageNumber, int pageSize, string searchName, string searchNation);
        Task<IEnumerable<Authors>> GetAllAuthorsAsync();
    }
}
