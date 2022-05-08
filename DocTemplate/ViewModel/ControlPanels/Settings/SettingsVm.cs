using CommunityToolkit.Mvvm.Input;
using DocTemplate.Global;
using DocTemplate.Helpers;
using DocTemplate.Model.API;
using DocTemplate.ServerHandler.API;
using Newtonsoft.Json;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

namespace DocTemplate.ViewModel.ControlPanels.Settings
{
    public class SettingsVm : ObservableObject
    {
        #region Команды
        public ICommand SaveAllCommand => new RelayCommand(SaveAll);
        public ICommand AddMinutesCommand => new RelayCommand(AddMinutes);
        public ICommand TakeMinutesCommand => new RelayCommand(TakeMinutes);
        public ICommand OpenFolderCommand => new RelayCommand(OpenFolder);
        public ICommand ChangeColorCommand => new RelayCommand<string>(ChangeColor);
        #endregion

        #region Данные
        private Properties.Settings _settings = Properties.Settings.Default;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _fileFormat;
        public string FileFormat
        {
            get => _fileFormat;
            set
            {
                _fileFormat = value;
                OnPropertyChanged();
            }
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        private int _autoSave;
        public int AutoSave
        {
            get => _autoSave;
            set
            {
                _autoSave = value;
                OnPropertyChanged();
            }
        }

        private bool _markEditable;
        public bool MarkEditable
        {
            get => _markEditable;
            set
            {
                _markEditable = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public SettingsVm()
        {
            Username = _settings.Username;
            FileFormat = _settings.DocFormat;
            FilePath = _settings.FilePath;
            AutoSave = _settings.AutoSave;
            MarkEditable = _settings.MarkEditable;

        }

        private void ChangeColor(string tag)
        {
            App.Color = tag;
        }

        private void AddMinutes() => AutoSave += 10;
        private void TakeMinutes() => AutoSave = AutoSave <= 0 ? AutoSave = 0 : AutoSave - 10;

        private void OpenFolder()
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
                FilePath = folder.SelectedPath;
        }
        private void SaveAll()
        {
            Thread thread = new Thread(x =>
            {
                if (Username != _settings.Username && InternetState.IsConnectedToInternet())
                {
                    var response = Requests.PutRequest("Users", DocTemplate.Properties.Settings.Default.UserID,
                        JsonConvert.SerializeObject(new Username
                        { CurrentName = _settings.Username, NewName = Username }));
                    if (response == GlobalConstants.SuccessMessage)
                        _settings.Username = Username;
                    else
                        MessageBox.Show(response);
                }
                if (FileFormat != _settings.DocFormat)
                    _settings.DocFormat = FileFormat;
                if (FilePath != _settings.FilePath)
                    _settings.FilePath = FilePath;
                if (AutoSave != _settings.AutoSave && AutoSave >= 0)
                    _settings.AutoSave = AutoSave;
                if (MarkEditable != _settings.MarkEditable)
                    _settings.MarkEditable = MarkEditable;
                _settings.Save();
            });
            thread.Start();
        }
    }
}
