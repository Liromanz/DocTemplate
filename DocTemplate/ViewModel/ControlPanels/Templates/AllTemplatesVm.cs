using System.Collections.Generic;
using System.Windows;
using DocTemplate.CardViews.Cards;
using DocTemplate.Global;
using DocTemplate.Helpers;
using DocTemplate.View;

namespace DocTemplate.ViewModel.ControlPanels.Templates
{
    public class AllTemplatesVm : ObservableObject
    {
        #region Команды

        #endregion

        #region Переменные
        private List<AdvancedTemplateCard> _cards;
        public List<AdvancedTemplateCard> Cards
        {
            get { return _cards; }
            set
            {
                _cards = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public AllTemplatesVm()
        {
            Cards = new List<AdvancedTemplateCard>();
            foreach (var template in DataContainers.PublicTemplates)
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
    }
}
