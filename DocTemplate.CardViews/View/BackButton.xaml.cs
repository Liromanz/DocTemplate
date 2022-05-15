using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DocTemplate.CardViews.View
{
    /// <summary>
    /// Логика взаимодействия для BackButton.xaml
    /// </summary>
    public partial class BackButton : UserControl
    {
        /// <summary>
        /// Метод для инициализации пользовательского элемента
        /// </summary>
        public BackButton()
        {
            InitializeComponent();
        }

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonCommandProperty =
            DependencyProperty.Register("ButtonCommand", typeof(ICommand), typeof(BackButton), new UIPropertyMetadata(null));
    }
}
