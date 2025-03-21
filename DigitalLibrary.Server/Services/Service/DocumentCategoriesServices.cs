using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class DocumentCategoriesServices : IDocumentCategoriesServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentCategoriesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddDocumentCategoriesAsync(DocumentCategories documentCategories)
        {
            var documentCategoriesExists = await FindDocumentCategoriesByDocumentCategoryIdAsync(documentCategories.documentid, documentCategories.categoryid);
            if (documentCategoriesExists != null)
            {
                throw new ArgumentException($"DocumentCategories with CategoryId {documentCategories.categoryid} does exist!");
            }

            await _unitOfWork.DocumentCategorise.AddAsync(documentCategories);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteDocumentCategories(int documentId, int cateId)
        {
            _unitOfWork.DocumentCategorise.DeleteRelationsAsync(documentId, cateId);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<DocumentCategories> FindDocumentCategoriesByDocumentCategoryIdAsync(int documentId, int cateId)
        {
            return await _unitOfWork.DocumentCategorise.GetDocumentCategoriesByDocumentCategoriesId(documentId, cateId);
        }

        public async Task<IEnumerable<DocumentCategories>> GetAllDocumentCategoriessAsync(int documentId)
        {
            return await _unitOfWork.DocumentCategorise.GetAllDocumentCategoriesByDocumentId(documentId);
        }
    }
}
