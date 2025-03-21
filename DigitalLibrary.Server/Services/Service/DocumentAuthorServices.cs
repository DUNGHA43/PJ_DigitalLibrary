using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class DocumentAuthorServices : IDocumentAuthorServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentAuthorServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddDocumentAuthorAsync(DocumentAuthors documentAuthor)
        {
            var documentAuthorExists = await _unitOfWork.DocumentAuthor.GetDocumentAuthorByDocumentAuthorId(documentAuthor.documentid, documentAuthor.authorid);

            if (documentAuthorExists != null)
            {
                throw new ArgumentException($"DocumentAuthor with AuthorId {documentAuthor.authorid} does exist!");
            }

            await _unitOfWork.DocumentAuthor.AddAsync(documentAuthor);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteDocumentAuthor(int documentId, int authorId)
        {
            _unitOfWork.DocumentAuthor.DeleteRelationsAsync(documentId, authorId);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<DocumentAuthors> FindDocumentAuthorByDocumentAuthorIdAsync(int documentId, int authorId)
        {
            return await _unitOfWork.DocumentAuthor.GetDocumentAuthorByDocumentAuthorId(documentId, authorId);
        }

        public async Task<IEnumerable<DocumentAuthors>> GetAllDocumentAuthorsAsync(int documentId)
        {
            return await _unitOfWork.DocumentAuthor.GetAllDocumentAuthorByDocumentId(documentId);
        }
    }
}
