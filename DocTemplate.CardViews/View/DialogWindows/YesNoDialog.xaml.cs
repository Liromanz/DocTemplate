using System.Windows;

namespace DocTemplate.CardViews.View.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для YesNoDialog.xaml
    /// </summary>
    public partial class YesNoDialog : Window
    {
        /// <summary>
        /// Метод для инициализации пользовательского элемента
        /// </summary>
        public YesNoDialog()
        {
            InitializeComponent();
        }
        public string DialogName
        {
            get => (string)GetValue(DialogNameProperty);
            set => SetValue(DialogNameProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DialogNameProperty =
            DependencyProperty.Register("DialogName", typeof(string), typeof(YesNoDialog), new UIPropertyMetadata(null));

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(YesNoDialog), new UIPropertyMetadata(null));

        /// <summary>
        /// Метод для сохранения результатов окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkDialog(object sender, RoutedEventArgs e) => DialogResult = true;
        /// <summary>
        /// Метод для закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseDialog(object sender, RoutedEventArgs e) => Close();
    }
}
