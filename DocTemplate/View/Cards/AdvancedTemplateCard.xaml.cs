using System.Windows;
using System.Windows.Controls;
using DocTemplate.Global.Models;

namespace DocTemplate.View.Cards
{
    /// <summary>
    /// Логика взаимодействия для AdvancedTemplateCard.xaml
    /// </summary>
    public partial class AdvancedTemplateCard : UserControl
    {
        public AdvancedTemplateCard()
        {
            InitializeComponent();
        }
        public Template TemplateCard
        {
            get => (Template)GetValue(TemplateCardProperty);
            set => SetValue(TemplateCardProperty, value);
        }
        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemplateCardProperty =
            DependencyProperty.Register("TemplateCard", typeof(Template), typeof(AdvancedTemplateCard), new UIPropertyMetadata(null));

    }
}
