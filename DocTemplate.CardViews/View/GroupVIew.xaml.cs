using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using DocTemplate.CardViews.View.DialogWindows;
using DocTemplate.Global.Models;

namespace DocTemplate.CardViews.View
{
    /// <summary>
    /// Логика взаимодействия для GroupVIew.xaml
    /// </summary>
    public partial class GroupVIew : UserControl
    {
        public GroupVIew()
        {
            InitializeComponent();
        }
        public string GroupName
        {
            get => (string)GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register("GroupName", typeof(string), typeof(GroupVIew), new UIPropertyMetadata(null));

        public ObservableCollection<object> GroupedTemplates
        {
            get => (ObservableCollection<object>)GetValue(GroupedTemplatesProperty);
            set => SetValue(GroupedTemplatesProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupedTemplatesProperty =
            DependencyProperty.Register("GroupedTemplates", typeof(ObservableCollection<object>), typeof(GroupVIew), new UIPropertyMetadata(null));


        private void EditGroup(object sender, RoutedEventArgs e)
        {
            var editDialog = new TypeInDialog();
            editDialog.ShowDialog();
        }

        private void DeleteGroup(object sender, RoutedEventArgs e)
        {
            var deleteDialog = new YesNoDialog();
            deleteDialog.ShowDialog();
        }

    }
}
