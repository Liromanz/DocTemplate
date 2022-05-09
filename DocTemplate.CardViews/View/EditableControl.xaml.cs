using System.Windows;
using System.Windows.Controls;

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

        public void AddTextBox(TextBox textBox)
        {
            textBox.Style = TryFindResource("DefaultTextBox") as Style;
            textBox.Height = double.NaN;
            ControlGrid.Children.Add(textBox);
            textBox.SetValue(Grid.RowProperty, 1);
        }
        public void AddComboBox(ComboBox comboBox)
        {
            comboBox.Style = TryFindResource("DefaultComboBox") as Style;
            ControlGrid.Children.Add(comboBox);
            comboBox.SetValue(Grid.RowProperty, 1);
        }
        public void AddDatePicker(DatePicker datePicker)
        {
            datePicker.Style = TryFindResource("DefaultDatePicker") as Style;
            ControlGrid.Children.Add(datePicker);
            datePicker.SetValue(Grid.RowProperty, 1);
        }
        public void AddButton(Button button)
        {
            button.Style = TryFindResource("ControlButton") as Style;
            button.Width = 200;
            ControlGrid.Children.Add(button);
            button.SetValue(Grid.RowProperty, 1);
        }
        public void AddWrapPanel(WrapPanel wrapPanel)
        {
            ControlGrid.Children.Add(wrapPanel);
            wrapPanel.SetValue(Grid.RowProperty, 1);
        }
    }
}
