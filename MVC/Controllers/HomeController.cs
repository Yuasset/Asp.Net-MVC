using Microsoft.AspNetCore.Mvc;
using MVC.Models.ViewModels;
using MVC.Services.Abstracts;
using MVC.Sessions;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductServiceManager productServiceManager;

        public HomeController(IProductServiceManager productServiceManager)
        {
            this.productServiceManager = productServiceManager;
        }
        public IActionResult Index()
        {
            return View(productServiceManager.GetAll());
        }
        public IActionResult AddToCard(int id)
        {
            var product = productServiceManager.GetById(id);
            if (product != null)
            {
                CardSession cardSession;
                if (SessionHelper.GetProductFromJson<CardSession>(HttpContext.Session, "sepet") == null)
                {
                    cardSession = new CardSession(); //Session içinde ilgili key değerine sahip bir nesne yoksa yeni bir tane oluştur
                }
                else
                {
                    cardSession = SessionHelper.GetProductFromJson<CardSession>(HttpContext.Session, "sepet"); //Session içinde ilgili key değerine sahip nesne varsa onu getir
                }

                CardVM cardVM = new CardVM //İlgili ürün bilgisini sepete taşıyacak CardVM nesnesinin propertysine Product nesnesini aktar.
                {
                    Product = product
                };

                cardSession.CardAddItem(cardVM); //Sepet listesini tutan CardSession nesnesine ürün ekle

                SessionHelper.SetFromProductJson(HttpContext.Session, "sepet", cardSession); //Session içine CardSession nesnesi json formatında string olarak ekle
            }
            return RedirectToAction("Index");
        }
        public IActionResult UpdateCard(int id, int quantity)
        {
            var card = SessionHelper.GetProductFromJson<CardSession>(HttpContext.Session, "sepet");
            card.CardUpdateItemQuantity(id, quantity);
            SessionHelper.SetFromProductJson(HttpContext.Session, "sepet", card);
            return RedirectToAction("Card");
        }
        public IActionResult Card()
        {
            var card = SessionHelper.GetProductFromJson<CardSession>(HttpContext.Session, "sepet");
            if (card != null)
            {
                return View(card);
            }
            return RedirectToAction("Index");
        }
    }
}
