using System.Windows;
using System.Windows.Controls;
using DocTemplate.CardViews.View;

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
        public string TemplateName
        {
            get => (string)GetValue(TemplateNameProperty);
            set => SetValue(TemplateNameProperty, value);
        }
        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemplateNameProperty =
            DependencyProperty.Register("TemplateName", typeof(string), typeof(AdvancedTemplateCard), new UIPropertyMetadata(null));

        public string TemplateTags
        {
            get => (string)GetValue(TemplateTagsProperty);
            set => SetValue(TemplateTagsProperty, value);
        }
        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemplateTagsProperty =
            DependencyProperty.Register("TemplateTags", typeof(string), typeof(AdvancedTemplateCard), new UIPropertyMetadata(null));
    }
}
