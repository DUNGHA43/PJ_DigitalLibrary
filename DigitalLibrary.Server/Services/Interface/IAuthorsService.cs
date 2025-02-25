using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IAuthorsService
    {
        Task<IEnumerable<Authors>> GetAllAuthorsAsync();
        Task<Authors> FindAuthorByIdAsync(int id);
        Task AddAuthorAsync(Authors author);
        Task UpdateAuthorAsync(Authors author);
        Task DeleteAuthorAsync(int id);
    }
}
