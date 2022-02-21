using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using DocTemplate.Helpers;

namespace DocTemplate.ViewModel.ControlPanels.Settings
{
    public class SettingsVm : ObservableObject
    {
        private RelayCommand<string> _changeColorCommand;
        public ICommand ChangeColorCommand => _changeColorCommand ?? (_changeColorCommand = new RelayCommand<string>(ChangeColor));

        public SettingsVm()
        {
            _changeColorCommand = new RelayCommand<string>(ChangeColor);
        }

        public void ChangeColor(string tag)
        { 
            App.Color = tag;
        }
    }
}
