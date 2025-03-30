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
        IDocumentAuthorsRepository DocumentAuthor { get; }
        IDocumentCategoriesRepository DocumentCategorise { get; }
        IDocumentSubjectsRepository DocumentSubject { get; }
        IDocumentRepository Documents { get; }
        IRepository<Nations> Nations { get; }
        IRepository<Roles> Roles { get; }
        IRepository<UserPermissions> UserPermissions { get; }
        IRepository<Statistics> Statistics { get; }
        Task SaveChangeAsync();
    }
}
