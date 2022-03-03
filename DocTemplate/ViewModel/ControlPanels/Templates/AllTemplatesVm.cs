using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Cards = new List<AdvancedTemplateCard>
            {
                new AdvancedTemplateCard{TemplateName = "Название", TemplateTags = "Темы"},
                new AdvancedTemplateCard{TemplateName = "Название", TemplateTags = "Темы"},
                new AdvancedTemplateCard{TemplateName = "Название", TemplateTags = "Темы"},
                new AdvancedTemplateCard{TemplateName = "Название", TemplateTags = "Темы"},
                new AdvancedTemplateCard{TemplateName = "Название", TemplateTags = "Темы"},
                new AdvancedTemplateCard{TemplateName = "Название", TemplateTags = "Темы"}
            };
        }
    }
}
