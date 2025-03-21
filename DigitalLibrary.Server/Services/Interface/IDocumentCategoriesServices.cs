using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IDocumentCategoriesServices
    {
        Task AddDocumentCategoriesAsync(DocumentCategories documentCategories);
        Task DeleteDocumentCategories(int documentId, int cateId);
        Task<IEnumerable<DocumentCategories>> GetAllDocumentCategoriessAsync(int documentId);
        Task<DocumentCategories> FindDocumentCategoriesByDocumentCategoryIdAsync(int documentId, int cateId);
    }
}
