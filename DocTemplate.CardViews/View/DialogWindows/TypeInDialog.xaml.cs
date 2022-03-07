using System;
using System.Windows;
using System.Windows.Input;

namespace DocTemplate.CardViews.View.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для TypeInDialog.xaml
    /// </summary>
    public partial class TypeInDialog : Window
    {
        public TypeInDialog()
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
            DependencyProperty.Register("DialogName", typeof(string), typeof(TypeInDialog), new UIPropertyMetadata(null));

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(TypeInDialog), new UIPropertyMetadata(null));

        public string HelperText
        {
            get => (string)GetValue(HelperTextProperty);
            set => SetValue(HelperTextProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HelperTextProperty =
            DependencyProperty.Register("HelperText", typeof(string), typeof(TypeInDialog), new UIPropertyMetadata(null));

        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("ButtonText", typeof(string), typeof(TypeInDialog), new UIPropertyMetadata(null));

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonCommandProperty =
            DependencyProperty.Register("ButtonCommand", typeof(ICommand), typeof(TypeInDialog), new UIPropertyMetadata(null));

        private void CloseDialog(object sender, RoutedEventArgs e) => Close();
    }
}
