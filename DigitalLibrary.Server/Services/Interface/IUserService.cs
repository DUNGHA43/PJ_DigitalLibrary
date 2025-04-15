using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users> FindUserByIdAsync(int id);
        Task AdduserAsync(Users user, IFormFile imgFile);
        Task UpdateUserAsync(Users user, IFormFile imgFile);
        Task UpdateUserAsync(Users user);
        Task DeleteUserAsync(int id);
        Users ValidateUser(string username, string password);
        Task<Users?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<Users> GetByEmailAsync(string email);
        Task<Users> GetByUsernameAsync(string username);
        Task<(IEnumerable<Users> Users, int TotalCount)> GetAllUsersAsync(int pageNumber, int pageSize, string searchName, string searchEmail, int? searchRole, bool? searchStatus);
        Task DeleteMultipleUsersAsync(List<int> userIds);
        Task<Users> FindUserByEmailAsync(string email);
        Task ChangePassAsync(ChangePassDTO ChangePassUser);
    }
}
