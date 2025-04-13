using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class TrafficLogServices : ITrafficLogServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public TrafficLogServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<List<TrafficLog>> GetTrafficLogs()
        {
            throw new NotImplementedException();
        }

        public async Task LogTraffic(TrafficLog log)
        {
            await _unitOfWork.TrafficLog.AddAsync(log);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
