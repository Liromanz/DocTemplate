using System.Collections.Generic;
using DocTemplate.Global.Models;
using DocTemplate.ServerHandler.API;
using Newtonsoft.Json;

namespace DocTemplate.Helpers
{
    public class DataContainers
    {
        public static List<Template> PublicTemplates =>
            InternetState.IsConnectedToInternet() ? 
                JsonConvert.DeserializeObject<List<Template>>(Requests.GetRequest("Templates")) : 
                new List<Template>();
    }
}
