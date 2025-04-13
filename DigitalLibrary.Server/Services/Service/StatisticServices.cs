using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Services.Service
{
    public class StatisticServices : IStatisticsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Statistics>> GetStatisticsAsync()
        {
            return await _unitOfWork.Statistics.GetAllAsync();
        }

        public Task<TrafficStatsDTO> GetStatsAsync(string period = "day")
        {
            return _unitOfWork.Statistics.GetStatsAsync(period);
        }

        public Task<ViewAndDowloadStatisticResponse> GetViewAndDowloadStatisticAsync()
        {
            return _unitOfWork.Statistics.GetViewAndDowloadStatisticAsync();
        }
    }
}
