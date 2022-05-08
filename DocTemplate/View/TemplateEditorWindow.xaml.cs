using DocTemplate.CardViews.View.DialogWindows;
using DocTemplate.Global.Models;
using DocTemplate.Helpers;
using DocTemplate.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace DocTemplate.View
{
    /// <summary>
    /// Логика взаимодействия для TemplateEditorWindow.xaml
    /// </summary>
    public partial class TemplateEditorWindow : Window
    {
        public TemplateEditorVm ViewModel => DataContext as TemplateEditorVm;
        private CheckBox[] _imageTypes;
        private CheckBox[] _fileTypes;
        public TemplateEditorWindow()
        {
            InitializeComponent();
            _fileTypes = new[] { TXTChk, CSChk, XAMLChk, HTMLChk, PDFChk, AllChk };
            ViewModel.FieldMetadatas = new List<FieldMetadata>();
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
            ViewModel.SerializeFieldData();
            DialogResult = true;
        }
        private void CursorChanged(object sender, RoutedEventArgs e)
        {
            var fullText = new TextRange(rtf.Document.ContentStart, rtf.Document.ContentEnd).Text;
            var allIndexes = fullText.FindAllIndexof('\u2063');
            var cursorPosition = new TextRange(rtf.Document.ContentStart, rtf.Selection.Start).Text.Length;
            for (int i = 0; i < allIndexes.Length - 1; i += 2)
            {
                if (allIndexes[i] <= cursorPosition && cursorPosition <= allIndexes[i + 1] ||
                    new TextRange(rtf.Selection.Start, rtf.Selection.End).Text.Contains('\u2063'))
                {
                    rtf.IsReadOnly = true;
                    FieldNameTxt.Text = fullText.Substring(allIndexes[i], allIndexes[i + 1] - allIndexes[i] + 1);
                    ViewModel.CurrentField = GetFieldName();

                    SetInterfaceByType();

                    EditableGrid.Visibility = Visibility.Visible;
                    AddingGrid.Visibility = Visibility.Collapsed;
                    break;
                }

                rtf.IsReadOnly = false;
                AddingGrid.Visibility = Visibility.Visible;
                ItemCollectionPanel.Visibility = Visibility.Collapsed;
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
            if (rtf != null && !rtf.Selection.IsEmpty)
            {
                var selectedBlocks = rtf.Document.Blocks.Where(x =>
                    x.ContentStart.CompareTo(rtf.CaretPosition) == -1 && x.ContentEnd.CompareTo(rtf.CaretPosition) == 1).ToArray();
                foreach (var block in selectedBlocks)
                {
                    rtf.SetValue(Block.LineHeightProperty, (double)LineSeparationCB.SelectedItem);
                    //rtf.Document.Blocks.First(x => x == block).LineHeight = (double)LineSeparationCB.SelectedItem;
                }
            }
        }

        private void ChangeStrikethrough(object sender, RoutedEventArgs e)
        {
            if (!rtf.Selection.IsEmpty)
                rtf.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Strikethrough);
            rtf.Focus();
        }

        #endregion

        #region Создание пунктов

        private void GenerateField(string fieldTypeName, Type fieldType)
        {
            TypeInDialog dialog = new TypeInDialog { DialogName = "Добавление нового поля", Placeholder = "Введите название этого поля. Для чего оно нужно?", ButtonText = "Создать" };
            if (dialog.ShowDialog() == true)
            {
                ViewModel.FieldMetadatas.Add(new FieldMetadata { Name = dialog.WroteText, FieldType = fieldType });
                ViewModel.CurrentField = dialog.WroteText;
                rtf.Selection.Text = $"\u2063{fieldTypeName} «{dialog.WroteText}»\u2063";
            }
            rtf.Focus();
        }

        private void AddTextBox(object sender, RoutedEventArgs e) => GenerateField("Текстовое поле", typeof(TextBox));
        private void AddComboBox(object sender, RoutedEventArgs e) => GenerateField("Список", typeof(ComboBox));
        private void AddNumer(object sender, RoutedEventArgs e) => GenerateField("Нумерация", typeof(TextBox));
        private void AddDate(object sender, RoutedEventArgs e) => GenerateField("Дата", typeof(DatePicker));
        private void AddTextFile(object sender, RoutedEventArgs e) => GenerateField("Текстовый файл", typeof(Button));
        private void AddImage(object sender, RoutedEventArgs e) => GenerateField("Фотография", typeof(Button));
        private void AddCheckBox(object sender, RoutedEventArgs e) => GenerateField("Множественный выбор", typeof(CheckBox));
        private void AddRadioButton(object sender, RoutedEventArgs e) => GenerateField("Единичный выбор", typeof(RadioButton));
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
                if (ViewModel.FieldMetadatas.Any(x => x.Name == dialog.WroteText))
                    MessageBox.Show("Поле с таким именем уже существует, имя должно быть уникальное");
                else
                {

                    var textRange = new TextRange(rtf.Document.ContentStart, rtf.Document.ContentEnd);
                    var newName = Regex.Replace(FieldNameTxt.Text, @"«.*»", $"«{dialog.WroteText}»");
                    ViewModel.CurrentField = GetFieldName();
                    ViewModel.FieldMetadatas.First(x => x.Name == GetFieldName()).Name = GetFieldName();
                    textRange.Text = textRange.Text.Replace(FieldNameTxt.Text, newName);
                    FieldNameTxt.Text = newName;
                }
            }

        }
        private void DeleteField(object sender, RoutedEventArgs e)
        {
            var deleteDialog = new YesNoDialog
            {
                DialogName = "Удаление поля",
                Description = "Вы уверены что хотите удалить поле? Если вы поставили поле вместо текста, вам придется вписать текст заново.",
            };

            if (deleteDialog.ShowDialog() == true)
            {
                var textRange = new TextRange(rtf.Document.ContentStart, rtf.Document.ContentEnd);
                textRange.Text = textRange.Text.Replace(FieldNameTxt.Text, "");
                var fieldData = ViewModel.FieldMetadatas.FirstOrDefault(x => x.Name == GetFieldName());
                if (fieldData != null)
                    ViewModel.FieldMetadatas.Remove(fieldData);

                rtf.IsReadOnly = false;
                AddingGrid.Visibility = Visibility.Visible;
                EditableGrid.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        private string GetFieldName()
        {
            var regex = new Regex(@"«.*»");
            return regex.Match(FieldNameTxt.Text).Value.Replace("«", "").Replace("»", "");
        }

        private void SetInterfaceByType()
        {
            NumerTypePanel.Visibility = Visibility.Collapsed;
            ItemCollectionPanel.Visibility = Visibility.Collapsed;
            DataMaskPanel.Visibility = Visibility.Collapsed;
            FileTypePanel.Visibility = Visibility.Collapsed;

            if (FieldNameTxt.Text.Contains("Список") || FieldNameTxt.Text.Contains("Множественный выбор") || FieldNameTxt.Text.Contains("Единичный выбор"))
            {
                var collection = ViewModel.FieldMetadatas.First(x => x.Name == GetFieldName()).ItemSource;
                if (collection.Length > 1)
                    ViewModel.ItemCollection = string.Join(", ", collection);
                else if (collection.Any())
                    ViewModel.ItemCollection = collection.First();
                ItemCollectionPanel.Visibility = Visibility.Visible;
            }
            if (FieldNameTxt.Text.Contains("Нумерация"))
            {
                ViewModel.NumerType = ViewModel.FieldMetadatas.First(x => x.Name == GetFieldName()).NumerType;
                NumerTypePanel.Visibility = Visibility.Visible;
            }

            if (FieldNameTxt.Text.Contains("Дата"))
            {
                ViewModel.DateType = ViewModel.FieldMetadatas.First(x => x.Name == GetFieldName()).DateMask;
                DataMaskPanel.Visibility = Visibility.Visible;
            }

            if (FieldNameTxt.Text.Contains("Текстовый файл"))
            {
                ViewModel.IsImage = false;
                foreach (var checkBox in _fileTypes)
                {
                    if (ViewModel.FieldMetadatas.First(x => x.Name == GetFieldName()).FileTypes
                        .Contains((string)checkBox.Content))
                        checkBox.IsChecked = true;
                }
                ViewModel.FileTypes = ViewModel.FieldMetadatas.First(x => x.Name == GetFieldName()).FileTypes;
                FileTypePanel.Visibility = Visibility.Visible;

            }
        }

        private void AddFileType(object sender, RoutedEventArgs e)
        {
            ViewModel.FileTypes += (sender as CheckBox).Content + "|";
        }
    }
}
