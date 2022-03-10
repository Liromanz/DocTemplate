using System.Windows;
using System.Windows.Input;

namespace DocTemplate.View
{
    /// <summary>
    /// Логика взаимодействия для TemplateCreatorWindow.xaml
    /// </summary>
    public partial class TemplateCreatorWindow : Window
    {
        public TemplateCreatorWindow()
        {
            InitializeComponent();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            Close();
        }
    }
}
