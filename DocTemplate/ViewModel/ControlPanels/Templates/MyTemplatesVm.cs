using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using DocTemplate.CardViews.Cards;
using DocTemplate.CardViews.Model;
using DocTemplate.CardViews.View;
using DocTemplate.CardViews.View.DialogWindows;
using DocTemplate.Global.Models;
using DocTemplate.Helpers;
using DocTemplate.ServerHandler.API;
using DocTemplate.View;
using Newtonsoft.Json;

namespace DocTemplate.ViewModel.ControlPanels.Templates
{
    public class MyTemplatesVm : ObservableObject
    {
        #region Команды
        public BindableCommand CreateGroupCommand { get; set; }

        #endregion

        #region Переменные

        private ObservableCollection<GroupView> _cards;
        public ObservableCollection<GroupView> Cards
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
            CreateGroupCommand = new BindableCommand(x => { GenerateField(); });

            Cards = CreateGroupsFromModel();

            if (InternetState.IsConnectedToInternet())
            {
                Cards.First().GroupedTemplates = CreateTemplatesFromModel((List<Template>)JsonConvert.DeserializeObject(
                   Requests.GetRequest($"Templates/{Properties.Settings.Default.UserID}"),
                   typeof(List<Template>)));
                DataContainers.UserGroupsModel = CreateModelFromCards(Cards);
            }
        }

        private void GenerateField()
        {
            TypeInDialog dialog = new TypeInDialog
            {
                DialogName = "Создание новой группы",
                Placeholder = "Введите имя новой группы",
                ButtonText = "Создать"
            };
            if (dialog.ShowDialog() == true)
            {
                var group = new GroupView
                {
                    GroupName = dialog.WroteText,
                    CanEditOrDelete = true,
                };
                group.ButtonCommand = new BindableCommand(x => { Cards.Remove(group);});
                Cards.Add(group);

                DataContainers.UserGroupsModel = CreateModelFromCards(Cards);
            }
        }

        private ObservableCollection<GroupView> CreateGroupsFromModel()
        {
            var userGroups = new ObservableCollection<GroupView>();
            foreach (var groupModel in DataContainers.UserGroupsModel)
            {
                var group = new GroupView
                {
                    GroupName = groupModel.GroupName,
                    CanEditOrDelete = groupModel.CanEditOrDelete,
                    GroupedTemplates = CreateTemplatesFromModel(groupModel.GroupedTemplates)
                };
                group.ButtonCommand = new BindableCommand(x => { Cards.Remove(group); });
                userGroups.Add(group);

            }

            if (!userGroups.Any())
            {
                userGroups = new ObservableCollection<GroupView>
                {
                    new GroupView
                    {
                        GroupName = "Созданные мной",
                        CanEditOrDelete = false
                    }
                };
            }

            return userGroups;
        }

        private ObservableCollection<TemplateCard> CreateTemplatesFromModel(List<Template> templates)
        {
            var templateCards = new ObservableCollection<TemplateCard>();
            if (templates == null) return templateCards;

            foreach (var templateModel in templates)
            {
                var clickCommand = new BindableCommand(x =>
                {
                    var documentWindow = new DocumentWindow { TemplateInfo = templateModel };
                    documentWindow.SetDataIntoFlowDocument(templateModel.FileText);
                    var current = Application.Current.MainWindow;
                    Application.Current.MainWindow = documentWindow;
                    Application.Current.MainWindow.Show();
                    current.Close();
                });

                var rightClickCommand = new BindableCommand(x =>
                {
                    var templateCreator = new TemplateCreatorWindow();
                    templateCreator.ViewModel.Template = templateModel;
                    var current = Application.Current.MainWindow;
                    Application.Current.MainWindow = templateCreator;
                    Application.Current.MainWindow.Show();
                    current.Close();
                });

                templateCards.Add(new TemplateCard
                    { TemplateInfo = templateModel, ClickCommand = clickCommand, RightClickCommand = rightClickCommand });
            }
            return templateCards;
        }

        public ObservableCollection<GroupViewModel> CreateModelFromCards(ObservableCollection<GroupView> cards)
        {
            var groups = new ObservableCollection<GroupViewModel>();
            foreach (var group in cards)
            {
                groups.Add(new GroupViewModel
                {
                    GroupName = group.GroupName,
                    CanEditOrDelete = group.CanEditOrDelete,
                    GroupedTemplates = group.GroupedTemplates?.Select(x => x.TemplateInfo).ToList()
                });
            }
            return groups;
        }
    }
}
