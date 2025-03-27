using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using System.Reflection.Metadata;

namespace DigitalLibrary.Server.Services.Service
{
    public class ReviewsServices : IReviewsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddReviewAsync(Reviews reviews)
        {
            await _unitOfWork.Reviews.AddAsync(reviews);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteReviewAsync(int id)
        {
            if (await FindReviewByIdAsync(id) == null)
            {
                throw new ArgumentException($"Review with id {id} dose not exist!");
            }

            _unitOfWork.Reviews.DeleteAsync(id);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<Reviews> FindReviewByIdAsync(int id)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(id);
            if (review == null)
            {
                throw new ArgumentException($"Review with id {id} dose not exist!");
            }

            return review;
        }

        public async Task<IEnumerable<Reviews>> FindReviewsByDocumentIdAsync(int documentId)
        {
            var reviews = await _unitOfWork.Reviews.FindReviewsByDocumentIdAsync(documentId);
            if(reviews == null)
            {
                throw new ArgumentException($"Reviews with documentid {documentId} dose not exist!");
            }
            return reviews;
        }

        public async Task<IEnumerable<Reviews>> FindReviewsByUserIdAsync(int userId)
        {
            var reviews = await _unitOfWork.Reviews.FindReviewsByUserIdAsync(userId);
            if (reviews == null)
            {
                throw new ArgumentException($"Reviews with documentid {userId} dose not exist!");
            }
            return reviews;
        }

        public async Task<IEnumerable<Reviews>> GetAllReviewsAsync()
        {
            return await _unitOfWork.Reviews.GetAllAsync();
        }

        public async Task EditReviewAsync(Reviews reviews)
        {
            var existingReview = await FindReviewByIdAsync(reviews.id);
            _unitOfWork.Reviews.EditAsync(reviews);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
