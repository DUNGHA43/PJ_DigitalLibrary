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

        public async Task UpdateStatistic(int documentId, string? update = null)
        {
            var statistic = await _unitOfWork.Statistics.FindStatisticByDocumentIdAsync(documentId);

            if (statistic == null)
            {
                throw new ArgumentException($"Statistic with documentId {documentId} does not exist!");
            }

            if (update == "view")
            {
                statistic.views++;
            }
            else if (update == "download")
            {
                statistic.dowloaded++;
            }
            else
            {
                throw new ArgumentException($"Invalid update type: {update}");
            }

            _unitOfWork.Statistics.EditAsync(statistic);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
