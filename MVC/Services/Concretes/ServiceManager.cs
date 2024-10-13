using MVC.Repositories.Abstracts;
using MVC.Services.Abstracts;

namespace MVC.Services.Concretes
{
    public class ServiceManager<T> : IServiceManager<T> where T : class
    {
        private readonly IRepository<T> repository;

        public ServiceManager(IRepository<T> repository)
        {
            this.repository = repository;
        }
        public async Task Create(T entity)
        {
            if (entity != null)
            {
                await repository.Create(entity);
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        public void Delete(T entity)
        {
            if (entity != null)
            {
                repository.Delete(entity);
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }

        public IEnumerable<T> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public T GetById(int id)
        {
            return repository.GetById(id);
        }

        public async Task Update(T entity)
        {
            if (entity != null)
            {
                await repository.Update(entity);
            }
            else
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }
    }
}
