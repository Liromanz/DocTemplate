using System.Windows;

namespace DocTemplate.CardViews.View.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для YesNoDialog.xaml
    /// </summary>
    public partial class YesNoDialog : Window
    {
        public YesNoDialog()
        {
            InitializeComponent();
        }

        private void CloseDialog(object sender, RoutedEventArgs e) => Close();
    }
}
