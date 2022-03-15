using System.Windows;
using System.Windows.Input;

namespace DocTemplate.View
{
    /// <summary>
    /// Логика взаимодействия для TemplateEditorWindow.xaml
    /// </summary>
    public partial class TemplateEditorWindow : Window
    {
        public TemplateEditorWindow()
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
            Application.Current.MainWindow = new TemplateCreatorWindow();
            Application.Current.MainWindow.Show();
            Close();
        }
    }
}
