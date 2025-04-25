using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.Repositorise.Interface;
using DigitalLibrary.Server.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
            var normalizedInput = username?.Trim().ToLower();
            var user = _context.users
                .Include(us => us.role)
                .AsNoTracking()
                .FirstOrDefault(us =>
                    us.email!.Trim().ToLower() == normalizedInput ||
                    us.username!.Trim().ToLower() == normalizedInput);

            if (user == null || !password.Trim().Equals(user.password!.Trim()))
            {
                return null!;
            }

            return user;
        }

        public async Task<Users> ValidateUserGoogle(string email, string name)
        {
            var user = _context.users
                .Include(us => us.role)
                .AsNoTracking()
                .FirstOrDefault(us => us.email!.Trim().ToLower() == email.Trim().ToLower());

            if (user == null)
            {
                var addUser = new Users
                {
                    fullname = name,
                    email = email,
                    password = "123456@",
                    roleid = 7,
                    status = true,
                    gender = true,
                    refreshtoken = null,
                    refreshtokenexpirytime = null,
                    createdate = DateTime.Now
                };

                await _dbSet.AddAsync(addUser);
                await _context.SaveChangesAsync();

                var UserAdded = await GetByEmailAsync(addUser.email!);
                if (UserAdded == null)
                {
                    throw new ArgumentException("user with this email does not exists!");
                }

                var subscription = new UserSubcriptions
                {
                    userid = UserAdded.id,
                    planid = 1,
                    redate = DateTime.Now,
                    exdate = DateTime.Now,
                    status = true
                };

                var userPermission = new UserPermissions
                {
                    userid = UserAdded.id,
                    canread = true,
                    candowload = false,
                };

                await _context.userPermissions.AddAsync(userPermission);
                await _context.userSubcriptions.AddAsync(subscription);
                await _context.SaveChangesAsync();
            }

            user = await _context.users
                .Include(us => us.role)
                .AsNoTracking()
                .FirstOrDefaultAsync(us => us.email!.Trim().ToLower() == email.Trim().ToLower());

            return user!;
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
