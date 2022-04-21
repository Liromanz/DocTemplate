using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DocTemplate.CardViews.Cards;
using DocTemplate.Global.Models;
using DocTemplate.Helpers;
using DocTemplate.View;

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

        public AllTemplatesVm()
        {
            SearchCommand = new BindableCommand(x => { FillListWithTemplate();});
            Cards = new ObservableCollection<AdvancedTemplateCard>();

            FillListWithTemplate();
        }

        private void FillListWithTemplate()
        {
            Cards.Clear();
            foreach (var template in GetTemplateList())
            {
                var openCommand = new BindableCommand(x =>
                {
                    var documentWindow = new DocumentWindow { TemplateInfo = template };
                    documentWindow.SetDataIntoFlowDocument(template.FileText);
                    var current = Application.Current.MainWindow;
                    Application.Current.MainWindow = documentWindow;
                    Application.Current.MainWindow.Show();
                    current.Close();
                });
                Cards.Add(new AdvancedTemplateCard { TemplateCard = template, OpenCommand = openCommand });
            }
        }

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
