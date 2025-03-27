using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class CategoriseServices : ICategoriseServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriseServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCategoryAsync(Categories category)
        {
            await _unitOfWork.Categorise.AddAsync(category);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var eixtingCate = await _unitOfWork.Categorise.GetByIdAsync(id);
            if (eixtingCate == null) {
                throw new ArgumentException($"Category with id {id} dose not exist!");
            }

            _unitOfWork.Categorise.DeleteAsync(id);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteMultipleAsync(List<int> categoryIds)
        {
            await _unitOfWork.Categorise.DeleteMultipleCategoriesAsync(categoryIds);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<Categories> FindCategoryByIdAsync(int id)
        {
            var existingCate = await _unitOfWork.Categorise.GetByIdAsync(id);
            if (existingCate == null)
            {
                throw new ArgumentException($"Category with id {id} dose not exist!");
            }
            return existingCate;
        }

        public async Task<(IEnumerable<Categories> Categories, int TotalCount)> GetAllCategoriesAsync(int pageNumber, int pageSize, string searchName)
        {
            return await _unitOfWork.Categorise.GetAllCategoriesAsync(pageNumber, pageSize, searchName);
        }

        public async Task<IEnumerable<Categories>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categorise.GetAllAsync();
            categories = categories.Where(c => c.status == true);
            return categories;
        }

        public async Task UpdateCategoryAsync(Categories category)
        {
            var existingCate = await _unitOfWork.Categorise.GetByIdAsync(category.id);

            if (existingCate == null)
            {
                throw new ArgumentException($"Category with id {category.id} does not exist!");
            }

            _unitOfWork.Categorise.EditAsync(category);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
