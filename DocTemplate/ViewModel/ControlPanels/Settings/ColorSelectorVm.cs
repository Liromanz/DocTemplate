using System.Windows.Media;
using DocTemplate.Helpers;

namespace DocTemplate.ViewModel.ControlPanels.Settings
{
    public class ColorSelectorVm : ObservableObject
    {
        public Brush BackColor { get; set; }
        public Brush MainColor { get; set; }
        public string Name { get; set; }

        public ColorSelectorVm()
        {
            BackColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#413F48");
            MainColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#ED927F");
            Name = "Персик";
        }

        public ColorSelectorVm(Brush backColor, Brush mainColor, string name)
        {
            BackColor = backColor;
            MainColor = mainColor;
            Name = name;
        }
    }
}
