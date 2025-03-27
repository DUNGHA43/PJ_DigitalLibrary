using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IUserPermissionServices
    {
        Task<IEnumerable<UserPermissions>> GetAllUserPermission();
        Task<UserPermissions> GetUserPermissionByIdAsync(int id);
        Task AddUserPermissionAsync(UserPermissions userPermissions);
        Task UpdateUserPermissionAsync(UserPermissions userPermissions);
    }
}
