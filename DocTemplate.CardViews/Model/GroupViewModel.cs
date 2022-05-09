using DocTemplate.Global.Models;
using System;
using System.Collections.Generic;

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
