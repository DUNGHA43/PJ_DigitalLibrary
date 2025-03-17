using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Data.Repositorise.Repository;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DbDigitalLibraryContext _context;
        public IUserRepository User { get; private set; }
        public IAuthorsRepository Authors { get; private set; }
        public ICategoriesRepository Categorise {  get; private set; }
        public ISubjectRepository Subject { get; private set; }
        public IReviewsRepository Reviews { get; private set; }
        public IRepository<DocumentAuthor> DocumentAuthor { get; private set; }
        public IRepository<DocumentCategorise> DocumentCategorise { get; private set; }
        public IRepository<DocumentSubject> DocumentSubject { get; private set; }
        public IDocumentRepository Documents { get; private set; }
        public IRepository<Nations> Nations { get; private set; }

        public UnitOfWork(DbDigitalLibraryContext context)
        {
            _context = context;
            User = new UserRepository(_context);
            Authors = new AuthorsRepository(_context);
            Categorise = new CategoriseRepository(_context);
            Subject = new SubjectRepository(_context);
            Reviews = new ReviewsRepository(_context);
            DocumentAuthor = new Repository<DocumentAuthor>(_context);
            DocumentCategorise = new Repository<DocumentCategorise>(_context);
            DocumentSubject = new Repository<DocumentSubject>(_context);
            Documents = new DocumentRepository(_context);
            Nations = new Repository<Nations>(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
