using DocTemplate.CardViews.View;
using DocTemplate.Global.Models;
using DocTemplate.Helpers;
using DocTemplate.ViewModel;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace DocTemplate.View
{
    /// <summary>
    /// Логика взаимодействия для DocumentWindow.xaml
    /// </summary>
    public partial class DocumentWindow : Window
    {
        public DocumentViewModel ViewModel => DataContext as DocumentViewModel;

        /// <summary>
        /// Метод для инициализации окна
        /// </summary>
        public DocumentWindow()
        {
            InitializeComponent();
            ViewModel.ThisWindow = this;
        }
        public Template TemplateInfo
        {
            get => (Template)GetValue(TemplateInfoProperty);
            set
            {
                SetValue(TemplateInfoProperty, value);
                ViewModel.FieldMetadatas = JsonConvert.DeserializeObject<List<FieldMetadata>>(TemplateInfo.FieldMetadata);
            }
        }

        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemplateInfoProperty =
            DependencyProperty.Register("TemplateInfo", typeof(Template), typeof(DocumentWindow), new UIPropertyMetadata(null));

        /// <summary>
        /// Метод для переноса окна мышкой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        /// <summary>
        /// Выгрузка RTF текста в FlowDocument
        /// </summary>
        /// <param name="text">RTF текст</param>
        public void SetDataIntoFlowDocument(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
                using (MemoryStream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(text)))
                {
                    textRange.Load(stream, DataFormats.Rtf);
                }
                foreach (var block in flowDocument.Blocks.Where(x => x.GetType() == typeof(Paragraph)))
                {
                    if (new TextRange(block.ContentStart, block.ContentEnd).Text.Contains("\u2063"))
                        CreateEditableField(block);
                }
            }
        }

        /// <summary>
        /// Создание изменяемого поля
        /// </summary>
        /// <param name="block">Блок, куда должно быть вставлено поле</param>
        private void CreateEditableField(Block block)
        {
            var editable = SelectFieldType(new TextRange(block.ContentStart, block.ContentEnd));

            StackPanel.Children.Add(editable);
        }

        /// <summary>
        /// Определение типа изменяемого поля
        /// </summary>
        /// <param name="txtRange">Текст, где находится изменяемое поле</param>
        /// <returns>Готовое изменяемое поле</returns>
        private EditableControl SelectFieldType(TextRange txtRange)
        {
            var textBlocks = txtRange.Text.Split("\u2063");

            object editableField;
            EditableControl editable = new EditableControl
            {
                ElementName = textBlocks[1].Substring(textBlocks[1].IndexOf('«') + 1, textBlocks[1].IndexOf('»') - textBlocks[1].IndexOf('«') - 1),
            };

            if (textBlocks[1].Contains("Текстовое поле"))
            {
                editableField = new TextBox();
                ((TextBox)editableField).TextChanged += (sender, args) =>
                {
                    textBlocks[1] = (editableField as TextBox)?.Text;
                    txtRange.Text = string.Join("\u2063", textBlocks);
                };
                ((TextBox)editableField).KeyDown += (sender, args) =>
                {
                    if (args.Key == Key.Enter)
                    {
                        (sender as TextBox).Text += Environment.NewLine;
                        (sender as TextBox).SelectionStart += (sender as TextBox).Text.Length;

                        textBlocks[1] += Environment.NewLine;
                    }
                };
                editable.AddTextBox((TextBox)editableField);
            }
            else if (textBlocks[1].Contains("Список"))
            {
                editableField = new ComboBox();
                ((ComboBox)editableField).ItemsSource = ViewModel.FieldMetadatas.First(x => x.Name == GetFieldName(textBlocks[1])).ItemSource;
                ((ComboBox)editableField).SelectionChanged += (sender, args) =>
                {
                    textBlocks[1] = (string)(editableField as ComboBox)?.SelectedValue;
                    txtRange.Text = string.Join("\u2063", textBlocks);
                };
                editable.AddComboBox((ComboBox)editableField);

            }
            else if (textBlocks[1].Contains("Нумерация"))
            {
                var mark = ViewModel.FieldMetadatas.First(x => x.Name == GetFieldName(textBlocks[1])).NumerType;
                editableField = new TextBox();
                ((TextBox)editableField).Text += $" {mark} ";
                ((TextBox)editableField).TextChanged += (sender, args) =>
                {
                    if (textBlocks[1].Contains("Нумерация"))
                    {
                        textBlocks[1] = $" {mark} {(editableField as TextBox)?.Text}";
                    }
                    else
                        textBlocks[1] = (editableField as TextBox)?.Text;
                    txtRange.Text = string.Join("\u2063", textBlocks);
                };
                ((TextBox)editableField).KeyDown += (sender, args) =>
                {
                    if (args.Key == Key.Enter)
                    {
                        if (mark == "•")
                        {
                            (sender as TextBox).Text += Environment.NewLine + " • ";
                            textBlocks[1] += Environment.NewLine + " • ";
                        }
                        if (mark == "1.")
                        {
                            (sender as TextBox).Text += Environment.NewLine + $" {textBlocks[1].Split(Environment.NewLine).Length + 1}. ";
                            textBlocks[1] += Environment.NewLine + $" {textBlocks[1].Split(Environment.NewLine).Length + 1}. ";
                        }
                        if (mark == "I.")
                        {
                            (sender as TextBox).Text += Environment.NewLine + $" {IntToRomanian.IntToRoman(textBlocks[1].Split(Environment.NewLine).Length + 1)}. ";
                            textBlocks[1] += Environment.NewLine + $" {IntToRomanian.IntToRoman(textBlocks[1].Split(Environment.NewLine).Length + 1)}. ";
                        }
                        (sender as TextBox).SelectionStart += (sender as TextBox).Text.Length;
                    }
                };
                editable.AddTextBox((TextBox)editableField);
            }
            else if (textBlocks[1].Contains("Дата"))
            {
                editableField = new DatePicker();
                var mask = ViewModel.FieldMetadatas.First(x => x.Name == GetFieldName(textBlocks[1])).DateMask;
                ((DatePicker)editableField).SelectedDateChanged += (sender, args) =>
                {
                    textBlocks[1] = (editableField as DatePicker)?.SelectedDate.Value.ToString(mask);
                    txtRange.Text = string.Join("\u2063", textBlocks);
                };
                editable.AddDatePicker((DatePicker)editableField);
            }
            else if (textBlocks[1].Contains("Текстовый файл"))
            {
                editableField = new Button();
                var filter = ViewModel.FieldMetadatas.First(x => x.Name == GetFieldName(textBlocks[1])).FileTypes;
                ((Button)editableField).Content = "Открыть файл...";
                ((Button)editableField).Click += (sender, args) =>
                {
                    OpenFileDialog fileDialog = new OpenFileDialog { Filter = filter };
                    if (fileDialog.ShowDialog() == true)
                    {
                        var text = File.ReadAllText(fileDialog.FileName);
                        textBlocks[1] = text;
                        txtRange.Text = string.Join("\u2063", textBlocks);
                    }
                };
                editable.AddButton((Button)editableField);
            }
            else if (textBlocks[1].Contains("Множественный выбор"))
            {
                var items = ViewModel.FieldMetadatas.First(x => x.Name == GetFieldName(textBlocks[1])).ItemSource;
                editableField = new WrapPanel { ItemWidth = 170, HorizontalAlignment = HorizontalAlignment.Center };
                foreach (var item in items)
                {
                    CheckBox checkBox = new CheckBox { Content = item, HorizontalAlignment = HorizontalAlignment.Center };
                    checkBox.Style = TryFindResource("DefaultCheckBox") as Style;

                    checkBox.Click += (sender, args) =>
                    {
                        if (textBlocks[1].Contains("Множественный выбор"))
                            textBlocks[1] = "";

                        if ((sender as CheckBox).IsChecked.Value)
                            textBlocks[1] += $", {item}";
                        if (!(sender as CheckBox).IsChecked.Value)
                        {
                            if (textBlocks[1].Contains($", {item}"))
                                textBlocks[1] = textBlocks[1].Replace($", {item}", "");
                            else if (textBlocks[1].Contains($"{item}"))
                                textBlocks[1] = textBlocks[1].Replace(item, "");
                        }

                        if (textBlocks[1] != "" && textBlocks[1].Substring(0, 2) == ", ")
                            textBlocks[1] = textBlocks[1].Substring(2);

                        txtRange.Text = string.Join("\u2063", textBlocks);
                    };
                    ((WrapPanel)editableField).Children.Add(checkBox);
                }
                editable.AddWrapPanel((WrapPanel)editableField);

            }
            else if (textBlocks[1].Contains("Единичный выбор"))
            {
                var items = ViewModel.FieldMetadatas.First(x => x.Name == GetFieldName(textBlocks[1])).ItemSource;
                editableField = new WrapPanel { ItemWidth = 170, HorizontalAlignment = HorizontalAlignment.Center };
                foreach (var item in items)
                {
                    RadioButton radio = new RadioButton { Content = item, GroupName = GetFieldName(textBlocks[1]), HorizontalAlignment = HorizontalAlignment.Center };
                    radio.Style = TryFindResource("DefaultRadioButton") as Style;

                    radio.Click += (sender, args) =>
                    {
                        (sender as RadioButton).IsChecked = true;
                        textBlocks[1] = item;
                        txtRange.Text = string.Join("\u2063", textBlocks);
                    };
                    ((WrapPanel)editableField).Children.Add(radio);
                }
                editable.AddWrapPanel((WrapPanel)editableField);
            }

            return editable;
        }

        /// <summary>
        /// Взятие имени поля из текста
        /// </summary>
        /// <param name="fullName">Текст, откуда идет взятие</param>
        /// <returns>Имя изменяемого поля</returns>
        private string GetFieldName(string fullName)
        {
            var regex = new Regex(@"«.*»");
            return regex.Match(fullName).Value.Replace("«", "").Replace("»", "");
        }
    }
}
