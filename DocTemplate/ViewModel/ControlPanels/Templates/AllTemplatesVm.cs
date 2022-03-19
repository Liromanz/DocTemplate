using System.Collections.Generic;
using DocTemplate.Global;
using DocTemplate.Helpers;
using DocTemplate.View.Cards;

namespace DocTemplate.ViewModel.ControlPanels.Templates
{
    public class AllTemplatesVm : ObservableObject
    {
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

        public AllTemplatesVm()
        {
            Cards = new List<AdvancedTemplateCard>();
            foreach (var template in DataContainers.PublicTemplates)
                Cards.Add(new AdvancedTemplateCard { TemplateCard = template });
        }
    }
}
