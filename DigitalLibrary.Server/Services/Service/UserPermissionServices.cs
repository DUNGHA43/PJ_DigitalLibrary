using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class UserPermissionServices : IUserPermissionServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserPermissionServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddUserPermissionAsync(UserPermissions userPermissions)
        {
            var existingUerPermission = await GetUserPermissionByIdAsync(userPermissions.userid);
            if (existingUerPermission != null)
            {
                throw new Exception("User Permission already exists");
            }

            await _unitOfWork.UserPermissions.AddAsync(userPermissions);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<IEnumerable<UserPermissions>> GetAllUserPermission()
        {
            return await _unitOfWork.UserPermissions.GetAllAsync();
        }

        public async Task<UserPermissions> GetUserPermissionByIdAsync(int id)
        {
            return await _unitOfWork.UserPermissions.GetByIdAsync(id);
        }

        public async Task UpdateUserPermissionAsync(UserPermissions userPermissions)
        {
            var existingUerPermission = await GetUserPermissionByIdAsync(userPermissions.userid);
            if (existingUerPermission == null)
            {
                throw new Exception("User Permission does not exist");
            }

            _unitOfWork.UserPermissions.EditAsync(userPermissions);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
