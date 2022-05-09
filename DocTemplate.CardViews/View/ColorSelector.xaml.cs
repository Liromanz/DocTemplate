using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DocTemplate.CardViews.View
{
    public partial class ColorSelector : UserControl
    {
        public ColorSelector()
        {
            InitializeComponent();
        }
        public Brush BackgroundColor
        {
            get => (Brush)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Brush), typeof(ColorSelector), new UIPropertyMetadata(null));

        public Brush MainColor
        {
            get => (Brush)GetValue(MainColorProperty);
            set => SetValue(MainColorProperty, value);
        }
        // Using a DependencyProperty as the backing store for MainColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainColorProperty =
            DependencyProperty.Register("MainColor", typeof(Brush), typeof(ColorSelector), new UIPropertyMetadata(null));

        public string ThemeName
        {
            get => (string)GetValue(ThemeNameProperty);
            set => SetValue(ThemeNameProperty, value);
        }
        // Using a DependencyProperty as the backing store for ThemeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThemeNameProperty =
            DependencyProperty.Register("ThemeName", typeof(string), typeof(ColorSelector), new UIPropertyMetadata(null));

        public string ColorName
        {
            get => (string)GetValue(ColorNameProperty);
            set => SetValue(ColorNameProperty, value);
        }
        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorNameProperty =
            DependencyProperty.Register("ColorName", typeof(Brush), typeof(ColorSelector), new UIPropertyMetadata(null));

    }
}
