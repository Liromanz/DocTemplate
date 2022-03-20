using System.Windows.Input;
using System.Windows.Media;
using DocTemplate.Helpers;

namespace DocTemplate.ViewModel
{
    public class TemplateEditorVm : ObservableObject
    {
        #region Команды
        public BindableCommand FontSizeCommand { get; set; }

        #endregion

        #region Переменные

        private FontFamily _fontFamily;
        public FontFamily FontFamily
        {
            get => _fontFamily ?? new FontFamily();
            set
            {
                _fontFamily = value;
                OnPropertyChanged();
            }
        }

        private string _selectedText;

        public string SelectedText
        {
            get => _selectedText;
            set
            {
                _selectedText = value; 
                OnPropertyChanged();
            }
        }

        #endregion

        public TemplateEditorVm()
        {
            
        }
    }
}
