using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IAuthorsRepository Authors { get; }
        ICategoriesRepository Categorise { get; }
        ISubjectRepository Subject { get; }
        IReviewsRepository Reviews { get; }
        IRepository<DocumentAuthor> DocumentAuthor { get; }
        IRepository<DocumentCategorise> DocumentCategorise { get; }
        IRepository<DocumentSubject> DocumentSubject { get; }
        IDocumentRepository Documents { get; }
        IRepository<Nations> Nations { get; }
        Task SaveChangeAsync();
    }
}
