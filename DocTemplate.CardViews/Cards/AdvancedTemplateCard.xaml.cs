using DocTemplate.Global.Models;
using DocTemplate.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace DocTemplate.CardViews.Cards
{
    /// <summary>
    /// Логика взаимодействия для AdvancedTemplateCard.xaml
    /// </summary>
    public partial class AdvancedTemplateCard : UserControl
    {
        /// <summary>
        /// Метод для инициализации пользовательского элемента
        /// </summary>
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
        public BindableCommand OpenCommand
        {
            get => (BindableCommand)GetValue(OpenCommandProperty);
            set => SetValue(OpenCommandProperty, value);
        }
        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenCommandProperty =
            DependencyProperty.Register("OpenCommand", typeof(BindableCommand), typeof(AdvancedTemplateCard), new UIPropertyMetadata(null));

    }
}
