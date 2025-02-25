using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AdduserAsync(Users user)
        {
            var existingUser = await _unitOfWork.User.GetByEmailAsync(user.email!);
            if (existingUser != null)
            {
                throw new ArgumentException("email already exists!");
            }
            await _unitOfWork.User.AddAsync(user);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteuserAsync(string email)
        {
            var user = await GetByEmailAsync(email);
            if (user == null)
            {
                throw new ArgumentException("user does not exits!");
            }
            _unitOfWork.User.DeleteAsync(user.id!);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _unitOfWork.User.GetAllAsync();
        }

        public async Task<Users?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _unitOfWork.User.GetUserByRefreshTokenAsync(refreshToken);
        }

        public async Task<Users> FindUserByIdAsync(string id)
        {
            return await _unitOfWork.User.GetByIdAsync(id);
        }

        public async Task UpdateuserAsync(Users user)
        {
            _unitOfWork.User.EditAsync(user);
            await _unitOfWork.SaveChangeAsync();
        }

        public Users ValidateUser(string username, string password)
        {
            return _unitOfWork.User.ValidateUser(username, password);
        }

        public async Task<Users> GetByEmailAsync(string email)
        {
            return await _unitOfWork.User.GetByEmailAsync(email);
        }

        public async Task<Users> GetByUsernameAsync(string username)
        {
            return await _unitOfWork.User.GetByUsernameAsync(username);
        }
    }
}
