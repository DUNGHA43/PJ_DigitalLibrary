using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IRolesServices
    {
        Task<IEnumerable<Roles>> GetAllRolesAsync();
    }
}
