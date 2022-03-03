using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using DocTemplate.Helpers;
using DocTemplate.ViewModel.ControlPanels.Settings;
using DocTemplate.ViewModel.ControlPanels.Templates;

namespace DocTemplate.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        #region Команды
        public BindableCommand AllTemplatesCommand { get; set; }
        public BindableCommand SettingsCommand { get; set; }

        #endregion

        #region Верски
        public AllTemplatesVm AllTemplatesVm { get; set; }
        public SettingsVm SettingsVm { get; set; }

        #endregion

        private bool _nothingSelected;
        public bool NothingSelected
        {
            get { return _nothingSelected; }
            set
            {
                _nothingSelected = value;
                OnPropertyChanged();
            }
        }


        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            NothingSelected = true;

            AllTemplatesVm = new AllTemplatesVm();
            SettingsVm = new SettingsVm();

            AllTemplatesCommand = new BindableCommand(o => { ChangeCurrentView(AllTemplatesVm); });
            SettingsCommand = new BindableCommand(o => { ChangeCurrentView(SettingsVm); });
        }

        private void ChangeCurrentView(object viewToChange)
        {
            CurrentView = viewToChange;
            NothingSelected = false;
        }
    }
}
