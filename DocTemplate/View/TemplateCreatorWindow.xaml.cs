using System.Windows;
using System.Windows.Input;
using DocTemplate.ViewModel;

namespace DocTemplate.View
{
    /// <summary>
    /// Логика взаимодействия для TemplateCreatorWindow.xaml
    /// </summary>
    public partial class TemplateCreatorWindow : Window
    {
        public TemplateCreatorVm ViewModel => DataContext as TemplateCreatorVm;

        public TemplateCreatorWindow()
        {
            InitializeComponent();
            ViewModel.ThisWindow = this;
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();

        }
    }
}
