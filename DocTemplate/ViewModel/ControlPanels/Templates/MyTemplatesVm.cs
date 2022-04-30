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
        public BindableCommand DeleteTemplateCommand { get; set; }

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
            CreateGroupCommand = new BindableCommand(x => { CreateGroup(); });

            Cards = CreateGroupsFromModel();

            if (InternetState.IsConnectedToInternet())
            {
                Cards.First().GroupedTemplates = CreateTemplatesFromModel((List<Template>)JsonConvert.DeserializeObject(
                   Requests.GetRequest($"Templates/{Properties.Settings.Default.UserID}"),
                   typeof(List<Template>)));
                DataContainers.UserGroupsModel = CreateModelFromCards(Cards);
            }
        }

        private void CreateGroup()
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
                group.ButtonCommand = new BindableCommand(x => { Cards.Remove(group); });
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
                var templateCard = new TemplateCard
                {
                    TemplateInfo = templateModel,
                    ClickCommand = new BindableCommand(x => { OpenCommand(templateModel); }),
                };

                #region Контекстное меню

                templateCard.MenuCreate.Click += (sender, args) => { OpenCommand(templateModel); };

                if (!templateModel.Editors.Contains(Properties.Settings.Default.Username) && templateModel.Editors != "All")
                    templateCard.MenuEdit.Visibility = Visibility.Collapsed;
                templateCard.MenuEdit.Click += (sender, args) =>
                {
                    var templateCreator = new TemplateCreatorWindow();
                    templateCreator.ViewModel.Template = templateModel;
                    var current = Application.Current.MainWindow;
                    Application.Current.MainWindow = templateCreator;
                    Application.Current.MainWindow.Show();
                    current.Close();
                };

                templateCard.MenuMove.Click += (sender, args) =>
                {
                    var items = Cards.Select(x => x.GroupName).ToList();
                    var startGroup = Cards.First(x => x.GroupedTemplates == templateCards).GroupName;
                    items.Remove(startGroup);
                    items.Remove("Созданные мной");
                    if (!items.Any())
                    {
                        MessageBox.Show("Нет ни одной группы, куда можно было бы перенести этот шаблон!");
                        return;
                    }

                    SelectorDialog dialog = new SelectorDialog
                    {
                        DialogName = "Перемещение шаблона",
                        ButtonText = "Переместить",
                        Items = items
                    };
                    if (dialog.ShowDialog().HasValue)
                    {
                        var finalGroup = (string)dialog.SelectionCbx.SelectedItem;
                        if (startGroup != "Созданные мной")
                            Cards.First(x => x.GroupName == startGroup).GroupedTemplates.Remove(templateCard);
                        Cards.First(x => x.GroupName == finalGroup).GroupedTemplates.Add(templateCard);
                    }
                };

                if (templateModel.IdUser != Properties.Settings.Default.UserID)
                    templateCard.MenuDelete.Visibility = Visibility.Collapsed;
                templateCard.MenuDelete.Click += (sender, args) =>
                    { Cards.First(x => x.GroupedTemplates == templateCards).GroupedTemplates.Remove(templateCard); };

                #endregion

                templateCards.Add(templateCard);

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

        private void OpenCommand(Template templateModel)
        {
            var documentWindow = new DocumentWindow { TemplateInfo = templateModel };
            documentWindow.SetDataIntoFlowDocument(templateModel.FileText);
            var current = Application.Current.MainWindow;
            Application.Current.MainWindow = documentWindow;
            Application.Current.MainWindow.Show();
            current.Close();
        }
    }
}
