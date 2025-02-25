using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users> FindUserByIdAsync(string id);
        Task AdduserAsync(Users user);
        Task UpdateuserAsync(Users user);
        Task DeleteuserAsync(string username);
        Users ValidateUser(string username, string password);
        Task<Users?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<Users> GetByEmailAsync(string email);
        Task<Users> GetByUsernameAsync(string username);
    }
}
