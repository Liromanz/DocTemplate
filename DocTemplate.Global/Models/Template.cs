#nullable disable

namespace DocTemplate.Global.Models
{
    public partial class Template
    {
        public int? IdTemplate { get; set; }
        public string Name { get; set; }
        public string Editors { get; set; }
        public string Users { get; set; }
        public string FileText { get; set; }
        public int IdUser { get; set; }
        public string Tags { get; set; }
    }
}
