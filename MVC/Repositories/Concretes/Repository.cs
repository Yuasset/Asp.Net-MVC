using MVC.Models.Market;
using MVC.Repositories.Abstracts;

namespace MVC.Repostory.Concretes
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MarketContext marketContext;

        public Repository(MarketContext marketContext)
        {
            this.marketContext = marketContext;
        }

        public async Task Create(T entity)
        {
            await marketContext.Set<T>().AddAsync(entity);
            await marketContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            marketContext.Remove(entity);
            marketContext.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return marketContext.Set<T>();
        }

        public T GetById(int id)
        {
            return marketContext.Set<T>().Find(id);
        }

        public async Task Update(T entity)
        {
            var data = await marketContext.Set<T>().FindAsync(entity);
            marketContext.Entry(data).CurrentValues.SetValues(entity);
            await marketContext.SaveChangesAsync();
        }
    }
}
