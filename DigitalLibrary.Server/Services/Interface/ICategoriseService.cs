using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface ICategoriseService
    {
        Task<IEnumerable<Categorise>> GetAllCategorisesAsync();
        Task<Categorise> FindCategoryByIdAsync(int id);
        Task AddCategoryAsync(Categorise category);
        Task UpdateCategoryAsync(Categorise category);
        Task DeleteCategoryAsync(int id);
    }
}
