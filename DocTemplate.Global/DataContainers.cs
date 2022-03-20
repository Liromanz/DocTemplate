using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using DocTemplate.Global.Models;

namespace DocTemplate.Global
{
    public class DataContainers
    {
        public static PropertyInfo[] ColorInfoList = typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);

        public static List<Template> PublicTemplates { get; set; } = new List<Template>();
    }
}
