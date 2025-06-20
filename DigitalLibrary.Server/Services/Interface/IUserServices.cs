﻿using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface IUserServices
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users> FindUserByIdAsync(int id);
        Task AdduserAsync(Users user, IFormFile? imgFile);
        Task UpdateUserAsync(Users user, IFormFile? imgFile);
        Task UpdateUserAsync(Users user);
        Task DeleteUserAsync(int id);
        Users ValidateUser(string username, string password);
        Task<Users> ValidateUserGoogle(string email, string name);
        Task<Users?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<Users> GetByEmailAsync(string email);
        Task<Users> GetByUsernameAsync(string username);
        Task<(IEnumerable<Users> Users, int TotalCount)> GetAllUsersAsync(int pageNumber, int pageSize, string searchName, string searchEmail, int? searchRole, bool? searchStatus);
        Task DeleteMultipleUsersAsync(List<int> userIds);
        Task<List<Users>> GetUsersByIdsAsync(List<int> userIds);

        Task ChangePassAsync(ChangePassDTO ChangePassUser);
    }
}
