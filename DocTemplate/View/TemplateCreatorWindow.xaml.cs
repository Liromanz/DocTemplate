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
    }
}
