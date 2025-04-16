using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace DigitalLibrary.Server.Services.Service
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadServices _uploadService;
        public UserServices(IUnitOfWork unitOfWork, IUploadServices uploadService)
        {
            _unitOfWork = unitOfWork;
            _uploadService = uploadService;
        }

        public async Task AdduserAsync(Users user, IFormFile? imgFile)
        {
            if (string.IsNullOrWhiteSpace(user.email))
            {
                throw new ArgumentException("Email cannot be empty!");
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(user.email, emailPattern))
            {
                throw new ArgumentException("Invalid email format!");
            }

            var existingUser = await _unitOfWork.User.GetByEmailAsync(user.email!);
            if (existingUser != null)
            {
                throw new ArgumentException("email already exists!");
            }

            var imageFile = new FileUpload();

            if (imgFile != null && imgFile.Length > 0)
            {
                imageFile = await _uploadService.UploadFileImageAsync(imgFile) ?? new FileUpload();
            }
            user.imageurl = imageFile.fileUrl ?? "";

            await _unitOfWork.User.AddAsync(user);
            await _unitOfWork.SaveChangeAsync();

            var UserAdded = await _unitOfWork.User.GetByEmailAsync(user.email!);
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

            await _unitOfWork.UserPermissions.AddAsync(userPermission);
            await _unitOfWork.UserSubscriptions.AddAsync(subscription);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await FindUserByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentException("user does not exits!");
            }
            if (!string.IsNullOrEmpty(user.imageurl))
            {
                await _uploadService.DeleteFileAsync(user.imageurl!);
            }
            _unitOfWork.User.DeleteAsync(user.id!);
            await _unitOfWork.SaveChangeAsync();
        }
        public async Task DeleteMultipleUsersAsync(List<int> userIds)
        {
            var users = await _unitOfWork.User.GetUsersByIdsAsync(userIds);

            if (users.Count != userIds.Count)
            {
                throw new ArgumentException("One or more users do not exist!");
            }

            foreach (var user in users)
            {
                if (!string.IsNullOrEmpty(user.imageurl))
                {
                    await _uploadService.DeleteFileAsync(user.imageurl!);
                }
            }

            await _unitOfWork.User.DeleteMultipleUsersAsync(userIds);
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

        public async Task<Users> FindUserByIdAsync(int id)
        {
            return await _unitOfWork.User.GetByIdAsync(id);
        }

        public async Task UpdateUserAsync(Users user, IFormFile? imgFile)
        {
            var existingUser = await GetByEmailAsync(user.email!);
            if (existingUser == null)
            {
                throw new ArgumentException("user does not exits!");
            }

            var imageFile = new FileUpload();
            try
            {
                if (imgFile != null && imgFile.Length > 0)
                {
                    imageFile = await _uploadService.UploadFileImageAsync(imgFile);
                    if(imageFile != null && !string.IsNullOrEmpty(existingUser.imageurl))
                    {
                        await _uploadService.DeleteFileAsync(existingUser.imageurl!);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error upload file image!", ex);
            }

            if (!string.IsNullOrEmpty(imageFile!.filePath))
            {
                user.imageurl = imageFile.fileUrl;
            }

            _unitOfWork.User.EditAsync(user);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateUserAsync(Users user)
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

        public async Task<(IEnumerable<Users> Users, int TotalCount)> GetAllUsersAsync(int pageNumber, int pageSize, string searchName, string searchEmail, int? searchRole, bool? searchStatus)
        {
            return await _unitOfWork.User.GetAllUsersAsync(pageNumber, pageSize, searchName, searchEmail, searchRole, searchStatus);
        }

        

        public async Task<List<Users>> GetUsersByIdsAsync(List<int> userIds)
        {
            return await _unitOfWork.User.GetUsersByIdsAsync(userIds);
        }

        public async Task ChangePassAsync(ChangePassDTO ChangePassUser)
        {
            var user = await _unitOfWork.User.GetByEmailAsync(ChangePassUser.email);

            if (user == null)
            {
                throw new ArgumentException("user does not exits!");
            }

            if (ChangePassUser.userid != user.id)
            {
                throw new ArgumentException("Authentication error!");
            }

            user.password = ChangePassUser.newPass;

            _unitOfWork.User.EditAsync(user);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
