using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface INationServices
    {
        Task<IEnumerable<Nations>> GetAllNationsAsync();
    }
}
