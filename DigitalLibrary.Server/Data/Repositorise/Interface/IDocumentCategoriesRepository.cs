using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IDocumentCategoriesRepository : IRepository<DocumentCategories>
    {
        Task<IEnumerable<DocumentCategories>> GetAllDocumentCategoriesByDocumentId(int documentId);
        Task<DocumentCategories> GetDocumentCategoriesByDocumentCategoriesId(int documentId, int cateId);
    }
}
