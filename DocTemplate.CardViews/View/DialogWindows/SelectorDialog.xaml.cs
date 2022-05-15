using System.Collections.Generic;
using System.Windows;

namespace DocTemplate.CardViews.View.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для SelectorDialog.xaml
    /// </summary>
    public partial class SelectorDialog : Window
    {
        /// <summary>
        /// Метод для инициализации пользовательского элемента
        /// </summary>
        public SelectorDialog()
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
            DependencyProperty.Register("DialogName", typeof(string), typeof(SelectorDialog), new UIPropertyMetadata(null));

        public List<string> Items
        {
            get => (List<string>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<string>), typeof(SelectorDialog), new UIPropertyMetadata(null));

        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("ButtonText", typeof(string), typeof(SelectorDialog), new UIPropertyMetadata(null));

        /// <summary>
        /// Метод для закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseDialog(object sender, RoutedEventArgs e) => Close();

        /// <summary>
        /// Метод для сохранения результатов окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OkDialog(object sender, RoutedEventArgs e) => DialogResult = true;
    }
}
