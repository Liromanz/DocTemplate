using System.Collections.Generic;
using System.Collections.ObjectModel;
using DocTemplate.CardViews.Model;
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

        public static ObservableCollection<GroupViewModel> UserGroupsModel = new ObservableCollection<GroupViewModel>();
    }
}
