using DocTemplate.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace DocTemplate.View
{
    /// <summary>
    /// Логика взаимодействия для TemplateCreatorWindow.xaml
    /// </summary>
    public partial class TemplateCreatorWindow : Window
    {
        public TemplateCreatorVm ViewModel => DataContext as TemplateCreatorVm;

        /// <summary>
        /// Метод для инициализации окна
        /// </summary>
        public TemplateCreatorWindow()
        {
            InitializeComponent();
            ViewModel.ThisWindow = this;
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
    }
}
