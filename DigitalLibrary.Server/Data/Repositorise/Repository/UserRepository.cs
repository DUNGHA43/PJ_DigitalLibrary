using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.Repositorise.Repository
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        private readonly DbDigitalLibraryContext _context;

        public UserRepository(DbDigitalLibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Users> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.email.Trim() == email.Trim());
        }

        public async Task<Users> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.username.Trim() == username.Trim());
        }

        public async Task<Users?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            var user = await _context.users.Include(us => us.role)
            .FirstOrDefaultAsync(u => u.refreshtoken!.Trim() == refreshToken.Trim()
                                   && u.refreshtokenexpirytime > DateTime.UtcNow);
            if (user == null)
            {
                return null;
            }

            return user;
        }

        public Users ValidateUser(string username, string password)
        {
            var user = _context.users.Include(us => us.role)
                .FirstOrDefault(us => us.username!.Trim() == username.Trim());

            if (user == null || !password.Trim().Equals(user.password!.Trim()))
            {
                return null!;
            }

            return user;
        }

        public async Task<(IEnumerable<Users> Users, int TotalCount)> GetAllUsersAsync(int pageNumber, int pageSize, string searchName, string searchEmail, int? searchRole, bool? searchStatus)
        {
            var query = _dbSet.Where(a => a.roleid != 1).AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(a => a.fullname!.Contains(searchName.Trim()));
            }

            if (!string.IsNullOrEmpty(searchEmail))
            {
                query = query.Where(a => a.email!.Contains(searchEmail.Trim()));
            }

            if (searchRole! != null)
            {
                query = query.Where(a => a.roleid == searchRole);
            }

            if (searchStatus != null) {
                query = query.Where(a => a.status == searchStatus);
            }

            var totalRecords = await query.CountAsync();

            var list = await query
                .OrderByDescending(a => a.createdate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (list, totalRecords);
        }

        public async Task DeleteMultipleUsersAsync(List<int> userIds)
        {
            await DeleteMultipleAsync(userIds, "id");
        }

        public async Task<List<Users>> GetUsersByIdsAsync(List<int> userIds)
        {
            return await _dbSet.Where(u => userIds.Contains(u.id)).ToListAsync();
        }
    }
}
