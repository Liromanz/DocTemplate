using System;
using DocTemplate.Global.Models;
using DocTemplate.Helpers;

namespace DocTemplate.CardViews.Model
{
    [Serializable]
    public class TemplateCardModel
    {
        public Template TemplateInfo { get; set; }
        public BindableCommand ClickCommand { get; set; }
        public BindableCommand RightClickCommand { get; set; }
    }
}
