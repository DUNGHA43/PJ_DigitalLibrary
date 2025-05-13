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
        public IDocumentAuthorsRepository DocumentAuthor { get; private set; }
        public IDocumentCategoriesRepository DocumentCategorise { get; private set; }
        public IDocumentSubjectsRepository DocumentSubject { get; private set; }
        public IDocumentRepository Documents { get; private set; }
        public IRepository<Nations> Nations { get; private set; }
        public IRepository<Roles> Roles { get; private set; }
        public IRepository<UserPermissions> UserPermissions { get; private set; }
        public IStatisticRepository Statistics { get; private set; }
        public IUserSubscriptionsRepository UserSubscriptions { get; private set; }
        public IPaymentHistoryRepository PaymentHistory { get; private set; }
        public IRepository<TrafficLog> TrafficLog { get; private set; }
        public IFavoListReppository FavoList { get; private set; }

        public UnitOfWork(DbDigitalLibraryContext context)
        {
            _context = context;
            User = new UserRepository(_context);
            Authors = new AuthorsRepository(_context);
            Categorise = new CategoriseRepository(_context);
            Subject = new SubjectRepository(_context);
            Reviews = new ReviewsRepository(_context);
            DocumentAuthor = new DocumentAuthorsRepository(_context);
            DocumentCategorise = new DocumentCategoriesRepository(_context);
            DocumentSubject = new DocumentSubjectsRepository(_context);
            Documents = new DocumentRepository(_context);
            Nations = new Repository<Nations>(_context);
            Roles = new Repository<Roles>(_context);
            UserPermissions = new Repository<UserPermissions>(_context);
            Statistics = new StatisticRepository(_context);
            UserSubscriptions = new UserSubscriptionsRepository(_context);
            PaymentHistory = new PaymentHistoryRepository(_context);
            TrafficLog = new Repository<TrafficLog>(_context);
            FavoList = new FavoListRepository(_context);
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
