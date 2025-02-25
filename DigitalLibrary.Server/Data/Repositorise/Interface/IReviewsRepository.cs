using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IReviewsRepository : IRepository<Reviews>
    {
        Task<IEnumerable<Reviews>> FindReviewsByUserIdAsync(int userid);
        Task<IEnumerable<Reviews>> FindReviewsByDocumentIdAsync(int documentid);
    }
}
