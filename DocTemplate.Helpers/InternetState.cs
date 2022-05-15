using System.Net;

namespace DocTemplate.Helpers
{
    public class InternetState
    {
        /// <summary>
        /// Проверка подключения компьютера к интернету
        /// </summary>
        /// <returns>Ответ да-нет</returns>
        public static bool IsConnectedToInternet()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
