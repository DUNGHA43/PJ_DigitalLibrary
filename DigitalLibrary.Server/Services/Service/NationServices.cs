using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class NationServices : INationServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public NationServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Nations>> GetAllNationsAsync()
        {
            return await _unitOfWork.Nations.GetAllAsync();
        }
    }
}
