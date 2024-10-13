using Newtonsoft.Json;

namespace MVC.Sessions
{
    public static class SessionHelper
    {
        public static void SetFromProductJson(ISession session, string key, object value)
        {
            var serialize = JsonConvert.SerializeObject(value); //Nesneyi json formatına çevir
            session.SetString(key, serialize); //Session'a ilgili key değerine göre json formatındaki nesneyi ekle
        }

        public static T GetProductFromJson<T>(ISession session, string key)
        {
            var result = session.GetString(key); //Session'dan key değerine göre string veriyi al
            if (result == null)
            {
                return default(T); //Eğer result null ise default değeri döndür.
            }
            else
            {
                var desrialize = JsonConvert.DeserializeObject<T>(result); //string gelen result değerini(verisini) T tipinde nesneye dönüştür.
                return desrialize;
            }
        }
    }
}
