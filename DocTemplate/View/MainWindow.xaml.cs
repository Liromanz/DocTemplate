using System.Windows;
using System.Windows.Input;
using DocTemplate.ViewModel;

namespace DocTemplate.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void CreateTemplate(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = new TemplateCreatorWindow();
            Application.Current.MainWindow.Show();
            Close();
        }
    }
}
