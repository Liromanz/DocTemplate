using DocTemplate.Global.Models;
using DocTemplate.Helpers;
using System;

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
