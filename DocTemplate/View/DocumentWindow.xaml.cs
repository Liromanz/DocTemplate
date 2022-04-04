using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using DocTemplate.Global.Models;

namespace DocTemplate.View
{
    /// <summary>
    /// Логика взаимодействия для DocumentWindow.xaml
    /// </summary>
    public partial class DocumentWindow : Window
    {
        public DocumentWindow()
        {
            InitializeComponent();
        }
        public Template TemplateInfo
        {
            get => (Template)GetValue(TemplateInfoProperty);
            set => SetValue(TemplateInfoProperty, value);
        }
        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemplateInfoProperty =
            DependencyProperty.Register("TemplateInfo", typeof(Template), typeof(DocumentWindow), new UIPropertyMetadata(null));

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public void SetDataIntoFlowDocument(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
                using (MemoryStream stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(text)))
                {
                    textRange.Load(stream, DataFormats.Rtf);
                }

                var templateCount = textRange.Text.Count(x => x == '\u2063') / 2;

                for (int i = 0; i < templateCount; i++)
                {
                    //ищу параграф с необходимым символом
                    var parag = new Paragraph();
                    foreach (Paragraph block in flowDocument.Blocks.Where(x => x.GetType() == typeof(Paragraph)))
                    {
                        var blockText = new TextRange(block.ContentStart, block.ContentEnd).Text;
                        if (blockText.Contains("\u2063"))
                            parag = block;
                    }

                    //создаю текстбокс, который будет менят текст
                    TextBox textBox = new TextBox();
                    textBox.TextChanged += (sender, args) =>
                    {
                        //беру значение из блока
                        var blockText = new TextRange(parag.ContentStart, parag.ContentEnd);
                        var separatedstring = blockText.Text.Split("\u2063").ToList();
                        //вставляю текст из текстбокса
                        separatedstring[1] = textBox.Text;
                        //возвращаю в блок
                        blockText.Text = string.Join("\u2063", separatedstring);
                    };

                    StackPanel.Children.Add(textBox);
                }

            }
        }
    }
}
