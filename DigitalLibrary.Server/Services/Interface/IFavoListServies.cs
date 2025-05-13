using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IFavoListServies
    {
        Task AddFavoListAsync(FavoList favoList);
        Task DeleteFavoListAsync(int documentId, int userId);
        Task<FavoList> FindFavoListAsync(int documentId, int userId);
        Task<IEnumerable<FavoListDTO>> GetFavoListByUserAsync(int userId);
    }
}
