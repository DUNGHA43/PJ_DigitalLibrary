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
            .FirstOrDefaultAsync(u => u.refreshtoken.Trim() == refreshToken.Trim()
                                   && u.refreshtokenexpirytime > DateTime.UtcNow);
            if (user == null)
            {
                return null;
            }

            return user;
        }

        public Task<Users> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Users ValidateUser(string username, string password)
        {
            var user = _context.users.Include(us => us.role)
                .FirstOrDefault(us => us.username.Trim() == username.Trim());

            if (user == null || !password.Trim().Equals(user.password.Trim()))
            {
                return null;
            }

            return user;
        }
    }
}
