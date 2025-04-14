using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IStatisticsServices
    {
        Task<IEnumerable<Statistics>> GetStatisticsAsync();
        Task<ViewAndDowloadStatisticResponse> GetViewAndDowloadStatisticAsync();
        Task<TrafficStatsDTO> GetStatsAsync(string period = "day");
        Task UpdateStatistic(int documentId, string? update = null);
    }
}
