using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class RolesServices : IRolesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public RolesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Roles>> GetAllRolesAsync()
        {
            return await _unitOfWork.Roles.GetAllAsync();
        }
    }
}
