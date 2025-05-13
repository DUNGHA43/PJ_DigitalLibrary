using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Services.Service
{
    public class FavoListServices : IFavoListServies
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoListServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddFavoListAsync(FavoList favoList)
        {
            var FavoListExists = await _unitOfWork.FavoList.GetFavoListByDocumentAndUserId(favoList.documentid, favoList.userid);
            if(FavoListExists != null)
            {
                throw new Exception("FavoList already exists!");
            }
            await _unitOfWork.FavoList.AddAsync(favoList);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteFavoListAsync(int documentId, int userId)
        {
            _unitOfWork.FavoList.DeleteRelationsAsync(userId, documentId);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<FavoList> FindFavoListAsync(int documentId, int userId)
        {
            return await _unitOfWork.FavoList.GetFavoListByDocumentAndUserId(documentId, userId);
        }

        public async Task<IEnumerable<FavoListDTO>> GetFavoListByUserAsync(int userId)
        {
            return await _unitOfWork.FavoList.ShowFavoListByUser(userId);
        }
    }
}
