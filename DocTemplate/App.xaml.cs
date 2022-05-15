using DocTemplate.CardViews.Model;
using DocTemplate.Helpers;
using DocTemplate.ServerHandler.API;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

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

        /// <summary>
        /// Метод для старта программы
        /// </summary>
        public App()
        {
            InitializeComponent();
            ColorChanged += App_ColorChanged;
            Color = DocTemplate.Properties.Settings.Default.AppTheme;

            var groupThread = new Thread(() =>
                        {
                            var path = $@"{Environment.CurrentDirectory}\UserGroupsModel.json";
                            if (!File.Exists(path))
                            {
                                var stream = File.Create(path);
                                stream.Close();
                            }
                            DataContainers.UserGroupsModel = JsonConvert.DeserializeObject<ObservableCollection<GroupViewModel>>(File.ReadAllText(path)) ?? new ObservableCollection<GroupViewModel>();
                        });
            groupThread.Start();

            if (InternetState.IsConnectedToInternet())
            {
                var thread = new Thread(() =>
                {
                    if (DocTemplate.Properties.Settings.Default.FirstTime)
                    {
                        DocTemplate.Properties.Settings.Default.Username = Requests.PostRequest("Users").Result;
                        DocTemplate.Properties.Settings.Default.UserID = Convert.ToInt32(Requests.GetRequest(
                        $"Users/UserById?name={DocTemplate.Properties.Settings.Default.Username}"));
                        DocTemplate.Properties.Settings.Default.DocFormat = ".docx";
                        DocTemplate.Properties.Settings.Default.FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        DocTemplate.Properties.Settings.Default.FirstTime = false;
                        DocTemplate.Properties.Settings.Default.Save();
                    }
                });
                thread.Start();
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

        /// <summary>
        /// Изменение цвета программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_ColorChanged(Object sender, EventArgs e)
        {
            DocTemplate.Properties.Settings.Default.AppTheme = Color;
            DocTemplate.Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Изменение ресурса для чтения цвета
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="startswith"></param>
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


        /// <summary>
        /// Событие при закрытии приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnExit(object sender, ExitEventArgs e)
        {
            File.WriteAllText($@"{Environment.CurrentDirectory}\UserGroupsModel.json",
                JsonConvert.SerializeObject(DataContainers.UserGroupsModel));
        }
    }
}
