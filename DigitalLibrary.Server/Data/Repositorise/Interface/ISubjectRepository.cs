using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface ISubjectRepository : IRepository<Subjects>
    {
        Task<Subjects> GetSubjectByNameAsync(string name);
    }
}
