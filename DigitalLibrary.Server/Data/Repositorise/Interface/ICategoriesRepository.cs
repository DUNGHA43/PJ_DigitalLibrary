using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface ICategoriesRepository : IRepository<Categories>
    {
        Task<(IEnumerable<Categories> Categorise, int TotalCount)> GetAllCategoriesAsync(int pageNumber, int pageSize, string searchName);
        Task DeleteMultipleCategoriesAsync(List<int> categoryIds);
    }
}
