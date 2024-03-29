﻿using System.Windows;

namespace DocTemplate.CardViews.View.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для TypeInDialog.xaml
    /// </summary>
    public partial class TypeInDialog : Window
    {
        /// <summary>
        /// Метод для инициализации пользовательского элемента
        /// </summary>
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

        public string WroteText
        {
            get => (string)GetValue(WroteTextProperty);
            set => SetValue(WroteTextProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WroteTextProperty =
            DependencyProperty.Register("WroteText", typeof(string), typeof(TypeInDialog), new UIPropertyMetadata(null));

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
