﻿using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;
using Shared.DTO;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IDocumentServices
    {
        Task<IEnumerable<Documents>> GetAllDocumentsAsync();
        Task<Documents> FindDocumentByIdAsync(int id);
        Task AddDocumentAsync(Documents document, IFormFile dcmFile, IFormFile imgFile);
        Task UpdateDocumentAsync(Documents document, IFormFile dcmFile, IFormFile imgFile);
        Task DeleteDocumentAsync(int id);
        Task<(IEnumerable<Documents> Documents, int TotalCount)> GetAllDocumentsAsync(int pageNumber, int pageSize, string searchName);
        Task DeleteMultipleDocumentsAsync(List<int> documetnsIds);
        Task<List<Documents>> GetDocumentsByIdsAsync(List<int> documentIds);
        Task<IEnumerable<Documents>> GetDocumentHomePageAsync(int? subjectId = null, int? authorId = null, int? categoryId = null, string? accesslevel = null, string? searchName = null);
        Task<IEnumerable<DocumentGroupDTO>> GetDocumentByFilterAsync(int? subjectId = null, int? authorId = null, int? categoryId = null,
        string? accesslevel = null, string? searchName = null, string? filterGroup = null, int page = 1, int pageSize = 10);
        Task<DocumentsDTO> FindDocumentDetailByIdAsync(int id);
    }
}
