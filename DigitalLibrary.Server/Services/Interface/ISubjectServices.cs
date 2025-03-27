using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface ISubjectServices
    {
        Task<IEnumerable<Subjects>> GetAllSubjectsAsync();
        Task<Subjects> FindSubjectByIdAsync(int id);
        Task AddSubjectAsync(Subjects subject);
        Task UpdateSubjectAsync(Subjects subject);
        Task DeleteSubjectAsync(int id);
        Task<Subjects> FindSubjectByNameAsync(string name);
        Task<(IEnumerable<Subjects> Subjects, int TotalCount)> GetAllSubjectsAsync(int pageNumber, int pageSize, string searchName);
        Task DeleteMultipleSubjectsAsync(List<int> subjectIds);
    }
}
