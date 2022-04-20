using System.Net;
using System.Runtime.InteropServices;

namespace DocTemplate.Helpers
{
    public class InternetState
    { 
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
