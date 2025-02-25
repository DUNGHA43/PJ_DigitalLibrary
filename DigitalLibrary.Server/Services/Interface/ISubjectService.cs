using DigitalLibrary.Server.Model;

namespace DigitalLibrary.Server.Services.Interface
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subjects>> GetAllSubjectsAsync();
        Task<Subjects> FindSubjectByIdAsync(int id);
        Task AddSubjectAsync(Subjects subject);
        Task UpdateSubjectAsync(Subjects subject);
        Task DeleteSubjectAsync(int id);
        Task<Subjects> FindSubjectByNameAsync(string name);
    }
}
