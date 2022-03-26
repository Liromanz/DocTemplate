using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using DocTemplate.CardViews.View.DialogWindows;
using DrawColor = System.Drawing.Color;

namespace DocTemplate.View
{
    /// <summary>
    /// Логика взаимодействия для TemplateEditorWindow.xaml
    /// </summary>
    public partial class TemplateEditorWindow : Window
    {
        TypeInDialog dialog = new TypeInDialog { DialogName = "Добавление нового поля", Placeholder = "Введите название этого поля. Для чего оно нужно?", ButtonText = "Создать" };
        public TemplateEditorWindow()
        {
            InitializeComponent();
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
                FontCB.Items.Add(fontFamily);
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

        #region Изменение RTF

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

        private void ChangeBackgroundColor(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (SelectionCB.SelectedColor != null && !rtf.Selection.IsEmpty)
                rtf.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush((Color)SelectionCB.SelectedColor));
            rtf.Focus();
        }

        private void ChangeForegroundColor(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (TextCB.SelectedColor != null && !rtf.Selection.IsEmpty)
                rtf.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush((Color)TextCB.SelectedColor));
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

        private void ChangeStrikethrough(object sender, RoutedEventArgs e)
        {
            if (!rtf.Selection.IsEmpty)
                rtf.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Strikethrough);
            rtf.Focus();
        }

        #endregion

        #region Создание пунктов

        private void AddTextBox(object sender, RoutedEventArgs e)
        {
            if (dialog.ShowDialog() == true)
                rtf.Selection.Text = $"\u2063Текстовое поле «{dialog.WroteText}»\u2063";
        }
        #endregion
    }
}
