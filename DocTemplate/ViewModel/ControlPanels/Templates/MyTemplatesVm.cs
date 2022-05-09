using DocTemplate.CardViews.Cards;
using DocTemplate.CardViews.Model;
using DocTemplate.CardViews.View;
using DocTemplate.CardViews.View.DialogWindows;
using DocTemplate.Global.Models;
using DocTemplate.Helpers;
using DocTemplate.ServerHandler.API;
using DocTemplate.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace DocTemplate.ViewModel.ControlPanels.Templates
{
    public class MyTemplatesVm : ObservableObject
    {
        #region Команды
        public BindableCommand SearchCommand { get; set; }
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

        private string _search = "";
        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                OnPropertyChanged();
            }
        }
        public bool SearchByName { get; set; } = true;
        public bool SearchByTemplate { get; set; }
        public bool ExactSearch { get; set; }
        public bool ExactRegister { get; set; }

        #endregion

        public MyTemplatesVm()
        {
            CreateGroupCommand = new BindableCommand(x => { CreateGroup(); });
            SearchCommand = new BindableCommand(x => { Cards = CreateGroupsFromModel(); });

            Cards = CreateGroupsFromModel();

            if (InternetState.IsConnectedToInternet())
            {
                UpdateCreatedTemplates();
                Cards.First().GroupedTemplates = CreateTemplatesFromModel((List<Template>)JsonConvert.DeserializeObject(
                    Requests.GetRequest($"Templates/{Properties.Settings.Default.UserID}"),
                    typeof(List<Template>)));
                RefreshTemplates((List<Template>)JsonConvert.DeserializeObject(
                    Requests.GetRequest($"Templates/UserAccess/{Properties.Settings.Default.Username}"),
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
            foreach (var groupModel in GetTemplateList())
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
                    if (dialog.ShowDialog() == true)
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
                {
                    var group = Cards.First(x => x.GroupedTemplates == templateCards);
                    if (group.GroupName != "Созданные мной")
                        Cards.First(x => x.GroupedTemplates == templateCards).GroupedTemplates.Remove(templateCard);
                    else if (InternetState.IsConnectedToInternet())
                    {
                        var deleteDialog = new YesNoDialog
                        {
                            DialogName = "Удаление поля",
                            Description = "Вы уверены что хотите удалить свой шаблон? Это действие необратимо",
                        };
                        if (deleteDialog.ShowDialog() == true)
                        {
                            Requests.DeleteRequest("Templates", templateCard.TemplateInfo.IdTemplate.Value);
                            var remainGroups = Cards.Where(x => x.GroupedTemplates.Contains(templateCard));
                            if (remainGroups.Any())
                                foreach (var remainGroup in remainGroups)
                                    Cards.First(x => x == remainGroup).GroupedTemplates.Remove(templateCard);

                            File.WriteAllText($@"{Environment.CurrentDirectory}\UserGroupsModel.json",
                                JsonConvert.SerializeObject(CreateModelFromCards(Cards)));
                        }
                    }
                };

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

        private void RefreshTemplates(List<Template> cards)
        {
            foreach (var groupView in Cards)
            {
                foreach (var templateCard in groupView.GroupedTemplates)
                {
                    Cards.First(x => x == groupView).GroupedTemplates.First(t => t == templateCard).TemplateInfo =
                        cards.First(x => x.IdTemplate == templateCard.TemplateInfo.IdTemplate);
                }
            }
        }

        private void UpdateCreatedTemplates()
        {
            foreach (var template in Cards.First().GroupedTemplates.Where(x => x.TemplateInfo.NeedToUpdate))
            {
                Requests.PutRequest("Templates", template.TemplateInfo.IdTemplate.Value, true, JsonConvert.SerializeObject(template.TemplateInfo));
            }
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

        private List<GroupViewModel> GetTemplateList()
        {
            var groups = DataContainers.UserGroupsModel.ToList();
            if (SearchByName)
                groups = groups.Where(x => x.GroupName.ToLower().Contains(Search.ToLower())).ToList();
            if (SearchByTemplate)
                groups = groups.Where(x => x.GroupedTemplates.Any(t => t.Name.ToLower().Contains(Search.ToLower()))).ToList();
            if (SearchByName && ExactSearch)
                groups = groups.Where(x => x.GroupName.ToLower() == Search.ToLower()).ToList();
            if (SearchByTemplate && ExactSearch)
                groups = groups.Where(x => x.GroupedTemplates.Any(t => t.Name.ToLower() == Search.ToLower())).ToList();
            if (SearchByName && ExactRegister)
                groups = groups.Where(x => x.GroupName.Contains(Search)).ToList();
            if (SearchByTemplate && ExactRegister)
                groups = groups.Where(x => x.GroupedTemplates.Any(t => t.Name.Contains(Search))).ToList();
            if (SearchByName && ExactSearch && ExactRegister)
                groups = groups.Where(x => x.GroupName == Search).ToList();
            if (SearchByTemplate && ExactSearch && ExactRegister)
                groups = groups.Where(x => x.GroupedTemplates.Any(t => t.Name == Search)).ToList();
            return groups;
        }

    }
}
