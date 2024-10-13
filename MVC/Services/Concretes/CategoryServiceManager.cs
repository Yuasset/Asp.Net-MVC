using MVC.Models.Market;
using MVC.Repositories.Abstracts;
using MVC.Services.Abstracts;

namespace MVC.Services.Concretes
{
    public class CategoryServiceManager : ServiceManager<Category>, ICategoryServiceManager
    {
        public CategoryServiceManager(IRepository<Category> repository) : base(repository)
        {
        }
    }
}
