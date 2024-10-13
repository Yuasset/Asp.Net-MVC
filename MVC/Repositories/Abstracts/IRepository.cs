namespace MVC.Repositories.Abstracts
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IQueryable<T> GetAll();
        Task Create(T entity);
        Task Update(T entity);
        void Delete(T entity);
    }
}
