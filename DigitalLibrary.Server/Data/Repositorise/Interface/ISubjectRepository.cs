using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface ISubjectRepository : IRepository<Subjects>
    {
        Task<Subjects> GetSubjectByNameAsync(string name);
        Task<(IEnumerable<Subjects> Subjects, int TotalCount)> GetAllSubjectsAsync(int pageNumber, int pageSize, string searchName);
        Task DeleteMultipleSubjectsAsync(List<int> subjectIds);
    }
}
