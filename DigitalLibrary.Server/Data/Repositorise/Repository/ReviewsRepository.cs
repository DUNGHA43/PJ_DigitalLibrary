using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class ReviewsRepository : Repository<Reviews>, IReviewsRepository
    {
        private readonly DbDigitalLibraryContext _context;

        public ReviewsRepository(DbDigitalLibraryContext context) : base(context) {
                _context = context;
        }

        public async Task<IEnumerable<Reviews>> FindReviewsByDocumentIdAsync(int userid)
        {
            var reviews = await _context.reviews.Where(u => u.userid == userid && u.status == true).ToListAsync();

            if(reviews == null)
            {
                return null;
            }

            return reviews;
        }

        public async Task<IEnumerable<Reviews>> FindReviewsByUserIdAsync(int documentid)
        {
            var reviews = await _context.reviews.Where(u => u.documentid == documentid && u.status == true).ToListAsync();

            if (reviews == null)
            {
                return null;
            }

            return reviews;
        }
    }
}
