using MVC.Models.ViewModels;

namespace MVC.Sessions
{
    public class CardSession
    {

        public Dictionary<int, CardVM> Card = new Dictionary<int, CardVM>(); //Sepet, ürünlerin listesi

        public void CardAddItem(CardVM cardVM)
        {
            if (Card.ContainsKey(cardVM.Product.ID)) //ContainsKey metodu ile sepetin içinde ürün var mı kontrol ediyoruz Dictionary tipindeki listenin key değerini kontrol ediliyor
            {
                Card[cardVM.Product.ID].Quantity += 1; //Sepette var olan ürünün key değerine göre buluyor ve miktarını 1 arttır
                return; //Metodu sonlandır
            }
            Card.Add(cardVM.Product.ID, cardVM); //Sepette ürün yoksa ilgili ürünü ekle
        }

        public void CardUpdateItemQuantity(int id, int quantity)
        {
            if (Card.ContainsKey(id))
            {
                Card[id].Quantity = quantity;
            }
        }

        public void CardRemoveItem(int id)
        {
            if (Card.ContainsKey(id))
            {
                Card.Remove(id);
            }
        }

        public void CardClear()
        {
            if (Card.Count > 0)
            {
                Card.Clear();
            }
        }
    }
}
