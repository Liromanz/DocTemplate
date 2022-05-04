using System;
using System.IO;
using System.Linq;
using System.Windows;
using Forms = System.Windows.Forms;
using DocTemplate.Helpers;

namespace DocTemplate.ViewModel
{
    public class TemplateEditorVm : ObservableObject
    {
        #region Команды
        public BindableCommand ImportDocxCommand { get; set; }
        public BindableCommand ReturnCommand { get; set; }

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
            ReturnCommand = new BindableCommand(o => ReturnToWindow());
        }

        private void ImportDocx()
        {
            Forms.OpenFileDialog fileDialog = new Forms.OpenFileDialog();
            fileDialog.Filter = "RTF файлы|*.rtf";
            if (fileDialog.ShowDialog() == Forms.DialogResult.OK)
            {
                RtfContent = File.ReadAllText(fileDialog.FileName);
            }
        }

        private void ReturnToWindow()
        {
            Application.Current.Windows.OfType<Window>().Single(x => x.IsActive).Close();
        }
    }
}
