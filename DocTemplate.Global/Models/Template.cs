﻿#nullable disable

namespace DocTemplate.Global.Models
{
    public partial class Template
    {
        public int? IdTemplate { get; set; }
        public string Name { get; set; } = "";
        public string Editors { get; set; } = "Me";
        public string Users { get; set; } = "Me";
        public string FileText { get; set; } = "";
        public int IdUser { get; set; }
        public string Tags { get; set; } = "";
        public string FieldMetadata { get; set; } = "";
    }
}
