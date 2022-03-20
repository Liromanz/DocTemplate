using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using DocTemplate.Global;
using DrawColor = System.Drawing.Color;

namespace DocTemplate.View
{
    /// <summary>
    /// Логика взаимодействия для TemplateEditorWindow.xaml
    /// </summary>
    public partial class TemplateEditorWindow : Window
    {
        public TemplateEditorWindow()
        {
            InitializeComponent();
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
                FontCB.Items.Add(fontFamily);

            FillWithColors(SelectionCB);
            FillWithColors(TextCB);
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = new TemplateCreatorWindow();
            Application.Current.MainWindow.Show();
            Close();
        }

        private void FillWithColors(ComboBox comboBox)
        {
            foreach (PropertyInfo c in DataContainers.ColorInfoList)
            {
                var color = DrawColor.FromName(c.Name);
                comboBox.Items.Add(new ComboBoxItem
                {
                    Content = c.Name,
                    Background = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B)),
                    BorderThickness = new Thickness(0)
                });
            }
        }
        private void CheckNumeric(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ChangeFontFamily(object sender, SelectionChangedEventArgs e)
        {
            if (FontCB.SelectedIndex != -1 && !rtf.Selection.IsEmpty)
                rtf.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, FontCB.SelectedItem);
            rtf.Focus();
        }

        private void ChangeBackgroundColor(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionCB.SelectedIndex != -1 && !rtf.Selection.IsEmpty)
                rtf.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, (SelectionCB.Items[SelectionCB.SelectedIndex] as ComboBoxItem).Background);
            rtf.Focus();
        }

        private void ChangeForegroundColor(object sender, SelectionChangedEventArgs e)
        {
            if (TextCB.SelectedIndex != -1 && !rtf.Selection.IsEmpty)
                rtf.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, (TextCB.Items[TextCB.SelectedIndex] as ComboBoxItem).Background);
            rtf.Focus();
        }

        private void ChangeFontSize(object sender, RoutedEventArgs e)
        {
            if (SizeTB.Text != String.Empty && !rtf.Selection.IsEmpty)
                rtf.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, SizeTB.Text);
            rtf.Focus();
        }

        private void ChangeLineSeparation(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
