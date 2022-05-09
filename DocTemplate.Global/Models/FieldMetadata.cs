using System;

namespace DocTemplate.Global.Models
{
    public class FieldMetadata
    {
        public Type FieldType { get; set; }
        public string Name { get; set; }
        public string NumerType { get; set; }
        public string[] ItemSource { get; set; } = { "" };
        public string DateMask { get; set; }
        public string FileTypes { get; set; } = "";

    }
}
