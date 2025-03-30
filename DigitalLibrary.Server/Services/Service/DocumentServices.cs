using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class DocumentServices : IDocumentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadServices _uploadService;

        public DocumentServices(IUnitOfWork unitOfWork, IUploadServices uploadService)
        {
            _unitOfWork = unitOfWork;
            _uploadService = uploadService;
        }

        public async Task AddDocumentAsync(Documents document, IFormFile dcmFile, IFormFile? imgFile)
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

            var statistics = new Statistics()
            {
                documentid = document.id,
                views = 0,
                dowloaded = 0,
            };

            await _unitOfWork.Statistics.AddAsync(statistics);
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
            var documents = await GetDocumentsByIdsAsync(documetnsIds);

            if(documents.Count != documetnsIds.Count)
            {
                throw new ArgumentException("One or more documents  do not exist!");
            }

            foreach (var document in documents)
            {
                if (!string.IsNullOrEmpty(document.imageurl))
                {
                    await _uploadService.DeleteFileAsync(document.imageurl!);
                }
                if (!string.IsNullOrEmpty(document.fileurl))
                {
                    await _uploadService.DeleteFileAsync(document.fileurl!);
                }
            }

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

        public async Task<IEnumerable<Documents>> GetDocumentHomePageAsync(int? subjectId = null, int? authorId = null, int? categoryId = null)
        {
            return await _unitOfWork.Documents.GetDocumentHomePageAsync(subjectId, authorId, categoryId);
        }

        public async Task<List<Documents>> GetDocumentsByIdsAsync(List<int> documentIds)
        {
            return await _unitOfWork.Documents.GetDocumentsByIdsAsync(documentIds);
        }

        public async Task UpdateDocumentAsync(Documents document, IFormFile? dcmFile, IFormFile? imgFile)
        {
            var existingDocument = await FindDocumentByIdAsync(document.id);
            if (existingDocument == null)
            {
                throw new ArgumentException($"Document with id {document.id} does not exist!");
            }

            var documentFile = new FileUpload();
            var imageFile = new FileUpload();

            try
            {
                if (dcmFile != null && dcmFile.Length > 0)
                {
                    documentFile = await _uploadService.UploadFileDataAsync(dcmFile);
                    if (documentFile != null && !string.IsNullOrEmpty(existingDocument.fileurl))
                    {
                        await _uploadService.DeleteFileAsync(existingDocument.fileurl!);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error upload file pdf!", ex);
            }

            try
            {
                if (imgFile != null && imgFile.Length > 0)
                {
                    imageFile = await _uploadService.UploadFileImageAsync(imgFile);
                    if (documentFile != null && !string.IsNullOrEmpty(existingDocument.imageurl))
                    {
                        await _uploadService.DeleteFileAsync(existingDocument.imageurl!);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error upload file image!", ex);
            }

            if (!string.IsNullOrEmpty(imageFile.filePath))
            {
                document.imagepath = imageFile.filePath;
                document.imageurl = imageFile.fileUrl;
            }

            if (!string.IsNullOrEmpty(documentFile.filePath))
            {
                document.filepath = documentFile.filePath;
                document.fileurl = documentFile.fileUrl;
            }

            _unitOfWork.Documents.EditAsync(document);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
