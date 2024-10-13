using MVC.Models.Market;

namespace MVC.Models.ViewModels
{
    public class CardVM
    {
        public CardVM()
        {
            Quantity = 1;
        }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal
        {
            get
            {
                return Product.Price * Quantity;
            }
        }
    }
}
