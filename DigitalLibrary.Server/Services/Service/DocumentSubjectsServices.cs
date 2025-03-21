using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Services.Service
{
    public class DocumentSubjectsServices : IDocumentSubjectsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentSubjectsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddDocumentSubjectAsync(DocumentSubject documentSubject)
        {
            var DocumentSubject = await FindDocumentSubjectByDocumentSubjectIdAsync(documentSubject.documentid, documentSubject.subjectid);
            if (DocumentSubject != null)
            {
                throw new ArgumentException($"DocumentSubject with SubjectId {documentSubject.subjectid} does exist!");
            }

            await _unitOfWork.DocumentSubject.AddAsync(documentSubject);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteDocumentSubject(int documentId, int subjectId)
        {
            _unitOfWork.DocumentSubject.DeleteRelationsAsync(documentId, subjectId);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<DocumentSubject> FindDocumentSubjectByDocumentSubjectIdAsync(int documentId, int subjectId)
        {
            return await _unitOfWork.DocumentSubject.GetDocumentSubjectByDocumentSubjectId(documentId, subjectId);
        }

        public Task<IEnumerable<DocumentSubject>> GetAllDocumentSubjectsAsync(int documentId)
        {
            return _unitOfWork.DocumentSubject.GetAllDocumentSubjectByDocumentId(documentId);
        }
    }
}
