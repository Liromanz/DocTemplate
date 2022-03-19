using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using DocTemplate.CardViews.Cards;
using DocTemplate.CardViews.View;

namespace DocTemplate.ViewModel.ControlPanels.Templates
{
    public class MyTemplatesVm : ObservableObject
    {
        private List<GroupVIew> _cards;

        public List<GroupVIew> Cards
        {
            get { return _cards; }
            set
            {
                _cards = value;
                OnPropertyChanged();
            }
        }

        public MyTemplatesVm()
        {
            Cards = new List<GroupVIew> { 
                new GroupVIew { GroupName = "Созданные мной", CanEditOrDelete = false, GroupedTemplates = new ObservableCollection<TemplateCard>{new TemplateCard{TemplateName = "Название", TemplateTags = "Теги"}}}, 
                new GroupVIew { GroupName = "Пример группы", CanEditOrDelete = true }, };
        }
    }
}
