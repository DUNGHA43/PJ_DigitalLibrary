using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface ICategoriseServices
    {
        Task<(IEnumerable<Categories> Categories, int TotalCount)> GetAllCategoriesAsync(int pageNumber, int pageSize, string searchName);
        Task<Categories> FindCategoryByIdAsync(int id);
        Task AddCategoryAsync(Categories category);
        Task UpdateCategoryAsync(Categories category);
        Task DeleteCategoryAsync(int id);
        Task DeleteMultipleAsync(List<int> categoryIds);
        Task<IEnumerable<Categories>> GetAllCategoriesAsync();
    }
}
