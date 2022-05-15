using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DocTemplate.CardViews.View
{
    /// <summary>
    /// Логика взаимодействия для WindowManager.xaml
    /// </summary>
    public partial class WindowManager : UserControl
    {
        /// <summary>
        /// Метод для инициализации пользовательского элемента
        /// </summary>
        public WindowManager()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Закрытие приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Расширение окна до максимума
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaximizeApplication(object sender, RoutedEventArgs e)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (currentWindow != null)
                currentWindow.WindowState = currentWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        /// <summary>
        /// Сворачивание приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void RollApplication(object sender, RoutedEventArgs e)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (currentWindow != null)
                currentWindow.WindowState = WindowState.Minimized;
        }


    }
}
