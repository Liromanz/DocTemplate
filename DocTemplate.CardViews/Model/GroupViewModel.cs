using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DocTemplate.CardViews.Cards;
using DocTemplate.Global.Models;

namespace DocTemplate.CardViews.Model
{
    [Serializable]
    public class GroupViewModel
    {
        public string GroupName { get; set; }
        public bool CanEditOrDelete { get; set; }
        public List<Template> GroupedTemplates { get; set; }
    }
}
