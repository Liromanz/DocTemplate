using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using DocTemplate.CardViews.Cards;
using DocTemplate.CardViews.View;
using DocTemplate.Global.Models;
using DocTemplate.Helpers;
using DocTemplate.ServerHandler.API;
using DocTemplate.View;
using Newtonsoft.Json;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace DocTemplate.ViewModel.ControlPanels.Templates
{
    public class MyTemplatesVm : ObservableObject
    {
        #region Команды

        #endregion

        #region Переменные
        public Window ThisWindow { get; set; }
        public ObservableCollection<TemplateCard> CreatedByMe { get; set; } = new ObservableCollection<TemplateCard>();

        private List<GroupVIew> _cards;
        public List<GroupVIew> Cards
        {
            get => _cards;
            set
            {
                _cards = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public MyTemplatesVm()
        {
            var templates = (ObservableCollection<Template>)JsonConvert.DeserializeObject(
                Requests.GetRequest($"Templates/{Properties.Settings.Default.UserID}"), typeof(ObservableCollection<Template>));
            foreach (var template in templates)
            {
                var clickCommand = new BindableCommand(x =>
                {
                    //var templateCreator = new TemplateCreatorWindow();
                    //templateCreator.ViewModel.Template = template;
                    var documentWindow = new DocumentWindow{TemplateInfo =  template};
                    documentWindow.SetDataIntoFlowDocument(template.FileText);
                    var current = Application.Current.MainWindow;
                    Application.Current.MainWindow = documentWindow;
                    Application.Current.MainWindow.Show();
                    current.Close();
                });

                var rightClickCommand = new BindableCommand(x =>
                {
                    var templateCreator = new TemplateCreatorWindow();
                    templateCreator.ViewModel.Template = template;
                    var current = Application.Current.MainWindow;
                    Application.Current.MainWindow = templateCreator;
                    Application.Current.MainWindow.Show();
                    current.Close();
                });

                CreatedByMe.Add(new TemplateCard { TemplateInfo = template, ClickCommand = clickCommand, RightClickCommand = rightClickCommand });
            }

            Cards = new List<GroupVIew>
            {
                new GroupVIew
                {
                    GroupName = "Созданные мной",
                    CanEditOrDelete = false,
                    GroupedTemplates = CreatedByMe
                }
            };
        }
    }
}
