using DigitalLibrary.Server.Model;
using DigitalLibrary.Shared.DTO;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IFavoListReppository : IRepository<FavoList>
    {
        Task<FavoList> GetFavoListByDocumentAndUserId(int documentId, int userId);
        Task<IEnumerable<FavoListDTO>> ShowFavoListByUser(int userId);
    }
}
