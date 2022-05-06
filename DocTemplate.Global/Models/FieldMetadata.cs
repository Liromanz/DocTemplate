using System;
using System.Collections.Generic;

namespace DocTemplate.Global.Models
{
    public class FieldMetadata
    {
        public Type FieldType { get; set; } 
        public string Name { get; set; }
        public bool CanBeMultiple { get; set; }
        public string[] ItemSource { get; set; } = {""};
        public string FileTypes { get; set; }

    }
}
