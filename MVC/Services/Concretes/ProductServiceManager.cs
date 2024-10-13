using MVC.Models.Market;
using MVC.Repositories.Abstracts;
using MVC.Services.Abstracts;

namespace MVC.Services.Concretes
{
    public class ProductServiceManager : ServiceManager<Product>, IProductServiceManager
    {
        public ProductServiceManager(IRepository<Product> repository) : base(repository)
        {
            //base ile üst sınıfın constructor'ına repository nesnesi gönderilir. Üst sınıftan türetilen sınıfın constructor'ı çalıştığında üst sınıfın constructor'ı da çalışır. Bu sebeple üst sınıfın ihtiyaç duyduğu repository nesnesi base() kullanılarak gönderilir.
        }
    }
}
