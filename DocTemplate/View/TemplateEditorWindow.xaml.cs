using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using DocTemplate.CardViews.View.DialogWindows;
using DocTemplate.Helpers;
using DocTemplate.ViewModel;

namespace DocTemplate.View
{
    /// <summary>
    /// Логика взаимодействия для TemplateEditorWindow.xaml
    /// </summary>
    public partial class TemplateEditorWindow : Window
    {
        public TemplateEditorVm ViewModel => DataContext as TemplateEditorVm;

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
            Close();
        }
        private void rtf_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var fullText = new TextRange(rtf.Document.ContentStart, rtf.Document.ContentEnd).Text;
            var allIndexes = fullText.FindAllIndexof('\u2063');
            var cursorPosition = new TextRange(rtf.Document.ContentStart, rtf.Selection.Start).Text.Length;
            for (int i = 0; i < allIndexes.Length - 1; i += 2)
            {
                if ((allIndexes[i] <= cursorPosition && cursorPosition <= allIndexes[i + 1]) ||
                    new TextRange(rtf.Selection.Start, rtf.Selection.End).Text.Contains('\u2063'))
                {
                    rtf.IsReadOnly = true;
                    FieldNameTxt.Text = fullText.Substring(allIndexes[i], allIndexes[i + 1] - allIndexes[i]+1);
                    EditableGrid.Visibility = Visibility.Visible;
                    AddingGrid.Visibility = Visibility.Collapsed;
                    break;
                }

                rtf.IsReadOnly = false;
                AddingGrid.Visibility = Visibility.Visible;
                EditableGrid.Visibility = Visibility.Collapsed;
            }
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

        private void GenerateField(string fieldType)
        {
            TypeInDialog dialog = new TypeInDialog { DialogName = "Добавление нового поля", Placeholder = "Введите название этого поля. Для чего оно нужно?", ButtonText = "Создать" };
            if (dialog.ShowDialog() == true)
                rtf.Selection.Text = $"\u2063{fieldType} «{dialog.WroteText}»\u2063";
            rtf.Focus();
        }

        private void AddTextBox(object sender, RoutedEventArgs e) => GenerateField("Текстовое поле");
        private void AddComboBox(object sender, RoutedEventArgs e) => GenerateField("Список");
        private void AddNumer(object sender, RoutedEventArgs e) => GenerateField("Нумерация");
        private void AddDate(object sender, RoutedEventArgs e) => GenerateField("Дата");
        private void AddTextFile(object sender, RoutedEventArgs e) => GenerateField("Текстовый файл");
        private void AddImage(object sender, RoutedEventArgs e) => GenerateField("Фотография");
        private void AddCheckBox(object sender, RoutedEventArgs e) => GenerateField("Множественный выбор");
        private void AddRadioButton(object sender, RoutedEventArgs e) => GenerateField("Единичный выбор");
        private void AddTable(object sender, RoutedEventArgs e) => GenerateField("Таблица");
        private void AddComplexNumer(object sender, RoutedEventArgs e) => GenerateField("Номерной список с описанием");
        private void AddComplexImage(object sender, RoutedEventArgs e) => GenerateField("Картинка с подписью");
        #endregion

        #region Редактирование полей
        private void EditFieldName(object sender, RoutedEventArgs e)
        {
            TypeInDialog dialog = new TypeInDialog
            {
                DialogName = "Изменение имени поля", 
                Placeholder = "Введите название этого поля. Для чего оно нужно?", 
                ButtonText = "Изменить"
            };

            if (dialog.ShowDialog() == true)
            {
                var textRange = new TextRange(rtf.Document.ContentStart, rtf.Document.ContentEnd);
                var newName = Regex.Replace(FieldNameTxt.Text, @"«.*»", $"«{dialog.WroteText}»");
                textRange.Text = textRange.Text.Replace(FieldNameTxt.Text, newName);
                FieldNameTxt.Text = newName;
            }

        }
        #endregion

        private void DeleteField(object sender, RoutedEventArgs e)
        {
            var deleteDialog = new YesNoDialog
            {
                DialogName = "Удаление поля",
                Description = "Вы уверены что хотите удалить поле? Если вы поставили поле вместо текста, вам придется вписать текст заново.",
            };

            if (deleteDialog.ShowDialog().HasValue)
            {
                var textRange = new TextRange(rtf.Document.ContentStart, rtf.Document.ContentEnd);
                textRange.Text = textRange.Text.Replace(FieldNameTxt.Text, "");
            }
        }
    }
}
