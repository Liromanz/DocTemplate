using System;
using System.IO;
using System.Windows.Forms;
using DocTemplate.Helpers;

namespace DocTemplate.ViewModel
{
    public class TemplateEditorVm : ObservableObject
    {
        #region Команды
        public BindableCommand ImportDocxCommand { get; set; }

        #endregion

        #region Переменные
        private string _rtfContent;

        public string RtfContent
        {
            get { return _rtfContent; }
            set
            {
                _rtfContent = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public TemplateEditorVm()
        {
            ImportDocxCommand = new BindableCommand(o => ImportDocx());
        }

        private void ImportDocx()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "RTF файлы|*.rtf";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                RtfContent = File.ReadAllText(fileDialog.FileName);
            }
        }
    }
}
