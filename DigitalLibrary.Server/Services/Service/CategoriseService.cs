using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class CategoriseService : ICategoriseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCategoryAsync(Categorise category)
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

        public async Task<Categorise> FindCategoryByIdAsync(int id)
        {
            var existingCate = await _unitOfWork.Categorise.GetByIdAsync(id);
            if (existingCate == null)
            {
                throw new ArgumentException($"Category with id {id} dose not exist!");
            }
            return existingCate;
        }

        public async Task<IEnumerable<Categorise>> GetAllCategorisesAsync()
        {
            return await _unitOfWork.Categorise.GetAllAsync();
        }

        public async Task UpdateCategoryAsync(Categorise category)
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
