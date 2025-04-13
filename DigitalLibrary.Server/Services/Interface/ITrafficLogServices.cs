using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface ITrafficLogServices
    {
        Task LogTraffic(TrafficLog log);
        Task<List<TrafficLog>> GetTrafficLogs();
    }
}
