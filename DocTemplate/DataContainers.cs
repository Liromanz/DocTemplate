using DocTemplate.CardViews.Model;
using DocTemplate.Global.Models;
using DocTemplate.ServerHandler.API;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DocTemplate.Helpers
{
    public class DataContainers
    {
        private static List<Template> _publicTemplates;
        public static List<Template> PublicTemplates
        {
            get
            {
                if (_publicTemplates == null)
                {
                    if (InternetState.IsConnectedToInternet())
                    {
                        var json = Requests.GetRequest("Templates");
                        _publicTemplates = JsonConvert.DeserializeObject<List<Template>>(json) ?? new List<Template>();
                    }
                    else
                        _publicTemplates = new List<Template>();
                }
                return _publicTemplates;
            }
            set => _publicTemplates = value;
        }

        public static ObservableCollection<GroupViewModel> UserGroupsModel = new ObservableCollection<GroupViewModel>();
    }
}
