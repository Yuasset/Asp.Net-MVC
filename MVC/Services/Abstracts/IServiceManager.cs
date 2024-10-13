namespace MVC.Services.Abstracts
{
    public interface IServiceManager<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        Task Create(T entity);
        Task Update(T entity);
        void Delete(T entity);
    }
}
