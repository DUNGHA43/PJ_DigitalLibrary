namespace DigitalLibrary.Server.Data.Repositorise.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task AddAsync(T entity);
        void EditAsync(T entity);
        void DeleteAsync(object id);
        Task SaveChangeAsync();
    }
}
