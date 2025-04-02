using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IStatisticsServices
    {
        Task<IEnumerable<Statistics>> GetStatisticsAsync();
    }
}
