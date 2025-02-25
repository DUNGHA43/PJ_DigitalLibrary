using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using Microsoft.VisualBasic;

namespace DigitalLibrary.Server.Services.Service
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAuthorAsync(Authors author)
        {
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var existingAuthor = await _unitOfWork.Authors.GetByIdAsync(id);
            if (existingAuthor == null) {
                throw new Exception($"Author with id {id} does not exist!");
            }

            _unitOfWork.Authors.DeleteAsync(id);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<Authors> FindAuthorByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null) {
                throw new ArgumentException($"Author with id {id} does not exist!");
            }

            return author;
        }

        public async Task<IEnumerable<Authors>> GetAllAuthorsAsync()
        {
            return await _unitOfWork.Authors.GetAllAsync();
        }

        public async Task UpdateAuthorAsync(Authors author)
        {
            var existingAuthor = await _unitOfWork.Authors.GetByIdAsync(author.id);

            if (existingAuthor == null) {
                throw new ArgumentException($"Author with id {author.id} does not exist!");
            }

            _unitOfWork.Authors.EditAsync(author);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
