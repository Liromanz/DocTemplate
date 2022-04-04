using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DocTemplate.CardViews.View.DialogWindows;

namespace DocTemplate.CardViews.View
{
    /// <summary>
    /// Логика взаимодействия для EditableControl.xaml
    /// </summary>
    public partial class EditableControl : UserControl
    {
        public EditableControl()
        {
            InitializeComponent();
        }

        public string ElementName
        {
            get => (string)GetValue(ElementNameProperty);
            set => SetValue(ElementNameProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElementNameProperty =
            DependencyProperty.Register("ElementName", typeof(string), typeof(EditableControl), new UIPropertyMetadata(null));

        public void AddElement(TextBox textBox)
        {
            textBox.Style = TryFindResource("DefaultTextBox") as Style;
            ControlGrid.Children.Add(textBox);
            textBox.SetValue(Grid.RowProperty,1);
        }
    }
}
