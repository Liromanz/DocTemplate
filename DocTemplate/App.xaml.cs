using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DocTemplate.ServerHandler.API;

namespace DocTemplate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Изменение цвета
        public static event EventHandler ColorChanged;
        #endregion

        public App()
        {
            InitializeComponent();
            ColorChanged += App_ColorChanged;
            Color = DocTemplate.Properties.Settings.Default.AppTheme;

            if (DocTemplate.Properties.Settings.Default.FirstTime)
            { 
                DocTemplate.Properties.Settings.Default.Username = Requests.PostRequest("Users").Result;
                DocTemplate.Properties.Settings.Default.FirstTime = false;
                DocTemplate.Properties.Settings.Default.Save();
            }
        }

        public static string Color
        {
            get => DocTemplate.Properties.Settings.Default.AppTheme;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));

                DocTemplate.Properties.Settings.Default.AppTheme = value;

                var dict = new ResourceDictionary { Source = new Uri($"Properties/Resources/Colors/{value}.xaml", UriKind.Relative) };
                ChangeDictionary(dict, "Properties/Resources/Colors/");

                ColorChanged(Application.Current, new EventArgs());
            }
        }
        private void App_ColorChanged(Object sender, EventArgs e)
        {
            DocTemplate.Properties.Settings.Default.AppTheme = Color;
            DocTemplate.Properties.Settings.Default.Save();
        }

        private static void ChangeDictionary(ResourceDictionary dict, string startswith)
        {
            var oldDict = (from d in Current.Resources.MergedDictionaries
                           where d.Source != null && d.Source.OriginalString.StartsWith(startswith)
                           select d).First();
            if (oldDict != null)
            {
                var ind = Current.Resources.MergedDictionaries.IndexOf(oldDict);
                Current.Resources.MergedDictionaries.Remove(oldDict);
                Current.Resources.MergedDictionaries.Insert(ind, dict);
            }
            else
                Current.Resources.MergedDictionaries.Add(dict);
        }
    }
}
