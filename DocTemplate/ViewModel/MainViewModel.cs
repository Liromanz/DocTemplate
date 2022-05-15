using DocTemplate.Helpers;
using DocTemplate.ViewModel.ControlPanels.Settings;
using DocTemplate.ViewModel.ControlPanels.Templates;

namespace DocTemplate.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        #region Команды
        public BindableCommand AllTemplatesCommand { get; set; }
        public BindableCommand MyTemplatesCommand { get; set; }
        public BindableCommand SettingsCommand { get; set; }
        public BindableCommand HelpCommand { get; set; }

        #endregion

        #region Верски
        public AllTemplatesVm AllTemplatesVm { get; set; }
        public MyTemplatesVm MyTemplatesVm { get; set; }
        public HelpVm HelpVm { get; set; }

        #endregion

        private bool _nothingSelected;
        public bool NothingSelected
        {
            get => _nothingSelected;
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
        /// <summary>
        /// Класс для связи модели с окном
        /// </summary>
        public MainViewModel()
        {
            NothingSelected = true;

            AllTemplatesVm = new AllTemplatesVm();
            MyTemplatesVm = new MyTemplatesVm();
            HelpVm = new HelpVm();

            AllTemplatesCommand = new BindableCommand(o => { ChangeCurrentView(AllTemplatesVm); });
            MyTemplatesCommand = new BindableCommand(o => { ChangeCurrentView(MyTemplatesVm); });
            SettingsCommand = new BindableCommand(o => { ChangeCurrentView(new SettingsVm()); });
            HelpCommand = new BindableCommand(o => { ChangeCurrentView(HelpVm); });
        }

        /// <summary>
        /// Изменение модели для отображения
        /// </summary>
        /// <param name="viewToChange">Модель для отображения</param>
        private void ChangeCurrentView(object viewToChange)
        {
            CurrentView = viewToChange;
            NothingSelected = false;
        }
    }
}
