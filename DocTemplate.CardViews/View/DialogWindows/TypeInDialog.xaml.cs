using System;
using System.Windows;

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

        private void CloseDialog(object sender, RoutedEventArgs e) => Close();
    }
}
