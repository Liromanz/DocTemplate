using DocTemplate.Helpers;
using DocTemplate.ViewModel.ControlPanels.Settings;

namespace DocTemplate.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        #region Команды
        public RelayCommand SettingsCommand { get; set; }

        #endregion

        #region Верски
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

            SettingsVm = new SettingsVm();

            SettingsCommand = new RelayCommand(o => {ChangeCurrentView(SettingsVm);});
        }

        private void ChangeCurrentView(object viewToChange)
        {
            CurrentView = viewToChange;
            NothingSelected = false;
        }
    }
}
