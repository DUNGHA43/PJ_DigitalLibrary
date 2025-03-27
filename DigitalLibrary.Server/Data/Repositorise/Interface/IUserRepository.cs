using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IUserRepository : IRepository<Users>
    { 
        Users ValidateUser(string username, string password);
        Task<Users?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<Users> GetByEmailAsync(string email);
        Task<Users> GetByUsernameAsync(string username);
        Task<(IEnumerable<Users> Users, int TotalCount)> GetAllUsersAsync(int pageNumber, int pageSize, string searchName, string searchEmail, int? searchRole, bool? searchStatus);
        Task DeleteMultipleUsersAsync(List<int> userIds);
        Task<List<Users>> GetUsersByIdsAsync(List<int> userIds);
    }
}
