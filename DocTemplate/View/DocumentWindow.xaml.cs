using System;
using System.IO;
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

                MessageBox.Show(textRange.Text.IndexOf("\u8291?").ToString());

                TextBox txt = new TextBox();
                StackPanel.Children.Add(txt);

            }
        }
    }
}
