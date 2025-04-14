using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IStatisticRepository : IRepository<Statistics>
    {
        Task<ViewAndDowloadStatisticResponse> GetViewAndDowloadStatisticAsync();
        Task<TrafficStatsDTO> GetStatsAsync(string period = "day");
        Task<Statistics> FindStatisticByDocumentIdAsync(int documentId);
    }
}
