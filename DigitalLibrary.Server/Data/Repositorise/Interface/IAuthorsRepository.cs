using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IAuthorsRepository : IRepository<Authors>
    {
        Task DeleteMultipleAuthorsAsync(List<int> authorIds);
        Task<(IEnumerable<Authors> Authors, int TotalCount)> GetAllAuthorsAsync(int pageNumber, int pageSize, string searchName, string searchNation);
    }
}
