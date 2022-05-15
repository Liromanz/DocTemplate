using System.Windows;
using System.Windows.Input;

namespace DocTemplate.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Метод для инициализации окна
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Метод для переноса окна мышкой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        /// <summary>
        /// Переход на окно создания шаблона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateTemplate(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = new TemplateCreatorWindow();
            Application.Current.MainWindow.Show();
            Close();
        }
    }
}
