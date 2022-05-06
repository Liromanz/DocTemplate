using System;
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
        public WindowManager()
        {
            InitializeComponent();
        }

        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MaximizeApplication(object sender, RoutedEventArgs e)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (currentWindow != null)
                currentWindow.WindowState = currentWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void RollApplication(object sender, RoutedEventArgs e)
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (currentWindow != null)
                currentWindow.WindowState = WindowState.Minimized;
        }

        
    }
}
