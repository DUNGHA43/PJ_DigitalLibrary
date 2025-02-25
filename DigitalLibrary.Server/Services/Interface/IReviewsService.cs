using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IReviewsService
    {
        Task<IEnumerable<Reviews>> GetAllReviewsAsync();
        Task<Reviews> FindReviewByIdAsync(int id);
        Task<IEnumerable<Reviews>> FindReviewsByUserIdAsync(int userId);
        Task<IEnumerable<Reviews>> FindReviewsByDocumentIdAsync(int documentId);
        Task AddReviewAsync(Reviews reviews);
        Task EditReviewAsync(Reviews reviews);
        Task DeleteReviewAsync(int id);
    }
}
