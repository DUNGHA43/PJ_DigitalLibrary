using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IUserRepository : IRepository<Users>
    { 
        Task<Users> Login(string username, string password);
        Users ValidateUser(string username, string password);
        Task<Users?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<Users> GetByEmailAsync(string email);
        Task<Users> GetByUsernameAsync(string username);
    }
}
