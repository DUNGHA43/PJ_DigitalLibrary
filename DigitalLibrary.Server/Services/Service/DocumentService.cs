using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadService _uploadService;

        public DocumentService(IUnitOfWork unitOfWork, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _uploadService = uploadService;
        }

        public async Task AddDocumentAsync(Documents document, IFormFile dcmFile, IFormFile imgFile)
        {
            var imageFile = new FileUpload();
            var documentFile = new FileUpload();

            if(!(dcmFile == null) || !(dcmFile.Length == 0))
            {
                documentFile = await _uploadService.UploadFileDataAsync(dcmFile);
            }

            if (!(imgFile == null) || !(imgFile.Length == 0))
            {
                imageFile = await _uploadService.UploadFileImageAsync(imgFile);
            }

            document.imagepath = imageFile.filePath ?? "";
            document.imageurl = imageFile.fileUrl ?? "";
            document.filepath = documentFile.filePath ?? "";
            document.fileurl = documentFile.fileUrl ?? "";

            await _unitOfWork.Documents.AddAsync(document);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteDocumentAsync(int id)
        {
            var document = await FindDocumentByIdAsync(id);

            await _uploadService.DeleteFileAsync(document.fileurl!);
            await _uploadService.DeleteFileAsync(document.imageurl!);

            _unitOfWork.Documents.DeleteAsync(document.id);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteMultipleDocumentsAsync(List<int> documetnsIds)
        {
            await _unitOfWork.Documents.DeleteMultipleDocumentsAsync(documetnsIds);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<Documents> FindDocumentByIdAsync(int id)
        {
            var document = await _unitOfWork.Documents.GetByIdAsync(id);

            if (document == null)
            {
                throw new ArgumentException($"Document with id {id} dose not exist!");
            }

            return document;
        }

        public async Task<IEnumerable<Documents>> GetAllDocumentsAsync()
        {
            return await _unitOfWork.Documents.GetAllAsync();
        }

        public async Task<(IEnumerable<Documents> Documents, int TotalCount)> GetAllDocumentsAsync(int pageNumber, int pageSize, string searchName)
        {
            return await _unitOfWork.Documents.GetAllDocumentsAsync(pageNumber, pageSize, searchName);
        }

        public Task UpdateDocumentAsync(Documents document)
        {
            throw new NotImplementedException();
        }
    }
}
