using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Model;
using DigitalLibrary.Server.Services.Interface;

namespace DigitalLibrary.Server.Services.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddSubjectAsync(Subjects subject)
        {
            await _unitOfWork.Subject.AddAsync(subject);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteMultipleSubjectsAsync(List<int> subjectIds)
        {
            await _unitOfWork.Subject.DeleteMultipleSubjectsAsync(subjectIds);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteSubjectAsync(int id)
        {
            var subject = await FindSubjectByIdAsync(id);

            if (subject == null)
            {
                throw new ArgumentException($"Subject with id {id} dose not exist!");
            }

            _unitOfWork.Subject.DeleteAsync(subject.id);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<Subjects> FindSubjectByIdAsync(int id)
        {
            return await _unitOfWork.Subject.GetByIdAsync(id);
        }

        public async Task<Subjects> FindSubjectByNameAsync(string name)
        {
            return await _unitOfWork.Subject.GetSubjectByNameAsync(name);
        }

        public async Task<IEnumerable<Subjects>> GetAllSubjectsAsync()
        {
            var subjects = await _unitOfWork.Subject.GetAllAsync();
            subjects = subjects.Where(s => s.status == true);
            return subjects;
        }

        public async Task<(IEnumerable<Subjects> Subjects, int TotalCount)> GetAllSubjectsAsync(int pageNumber, int pageSize, string searchName)
        {
            return await _unitOfWork.Subject.GetAllSubjectsAsync(pageNumber, pageSize, searchName);
        }

        public async Task UpdateSubjectAsync(Subjects subject)
        {
            var existingSubject = await _unitOfWork.Subject.GetByIdAsync(subject.id);
            if (existingSubject == null)
            {
                throw new ArgumentException($"Subject with id {subject.id} dose not exist!");
            }

            _unitOfWork.Subject.EditAsync(existingSubject);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
