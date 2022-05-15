using DocTemplate.CardViews.Cards;
using DocTemplate.CardViews.View.DialogWindows;
using DocTemplate.Global.Models;
using DocTemplate.Helpers;
using DocTemplate.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DocTemplate.ViewModel.ControlPanels.Templates
{
    public class AllTemplatesVm : ObservableObject
    {
        #region Команды
        public BindableCommand SearchCommand { get; set; }

        #endregion

        #region Переменные
        private ObservableCollection<AdvancedTemplateCard> _cards;
        public ObservableCollection<AdvancedTemplateCard> Cards
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
        public bool SearchByTags { get; set; }
        public bool ExactSearch { get; set; }
        public bool ExactRegister { get; set; }

        #endregion

        /// <summary>
        /// Класс для связи модели с окном
        /// </summary>
        public AllTemplatesVm()
        {
            SearchCommand = new BindableCommand(x => { FillListWithTemplate(); });
            Cards = new ObservableCollection<AdvancedTemplateCard>();

            FillListWithTemplate();
        }
        /// <summary>
        /// Заполнение интерфейса публичными шаблонами
        /// </summary>
        private void FillListWithTemplate()
        {
            Cards.Clear();
            var templates = GetTemplateList();
            foreach (var template in templates)
            {
                var templateCard = new AdvancedTemplateCard
                {
                    TemplateCard = template,
                    OpenCommand = new BindableCommand(x =>
                    {
                        var documentWindow = new DocumentWindow { TemplateInfo = template };
                        documentWindow.SetDataIntoFlowDocument(template.FileText);
                        var current = Application.Current.MainWindow;
                        Application.Current.MainWindow = documentWindow;
                        Application.Current.MainWindow.Show();
                        current.Close();
                    })
                };
                templateCard.SaveButton.Click += (sender, args) =>
                {
                    var items = DataContainers.UserGroupsModel.Select(x => x.GroupName).ToList();
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
                        DataContainers.UserGroupsModel.First(x => x.GroupName == finalGroup).GroupedTemplates.Add(template);
                    }
                };
                Cards.Add(templateCard);
            }
        }

        /// <summary>
        /// Поиск шаблонов по параметрам
        /// </summary>
        /// <returns></returns>
        private List<Template> GetTemplateList()
        {
            var templates = DataContainers.PublicTemplates;
            if (SearchByName)
                templates = templates.Where(x => x.Name.ToLower().Contains(Search.ToLower())).ToList();
            if (SearchByTags)
                templates = templates.Where(x => x.Tags.ToLower().Contains(Search.ToLower())).ToList();
            if (SearchByName && ExactSearch)
                templates = templates.Where(x => x.Name.ToLower() == Search.ToLower()).ToList();
            if (SearchByTags && ExactSearch)
                templates = templates.Where(x => x.Tags.ToLower() == Search.ToLower()).ToList();
            if (SearchByName && ExactRegister)
                templates = templates.Where(x => x.Name.Contains(Search)).ToList();
            if (SearchByTags && ExactRegister)
                templates = templates.Where(x => x.Tags.Contains(Search)).ToList();
            if (SearchByName && ExactSearch && ExactRegister)
                templates = templates.Where(x => x.Name == Search).ToList();
            if (SearchByTags && ExactSearch && ExactRegister)
                templates = templates.Where(x => x.Tags == Search).ToList();
            return templates;
        }
    }
}
