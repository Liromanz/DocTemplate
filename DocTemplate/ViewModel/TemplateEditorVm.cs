using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using DocTemplate.Global.Models;
using Forms = System.Windows.Forms;
using DocTemplate.Helpers;
using Newtonsoft.Json;

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
            get => _rtfContent;
            set
            {
                _rtfContent = value;
                OnPropertyChanged();
            }
        }

        public string CurrentField { get; set; }

        public List<FieldMetadata> FieldMetadatas { get; set; } = new List<FieldMetadata>();

        public string JsonMetadata { get; set; }

        private string _itemCollection = "";

        public string ItemCollection
        {
            get => _itemCollection;
            set
            {
                _itemCollection = value; 
                FieldMetadatas.First(x => x.Name == CurrentField).ItemSource = value.Split(", ");
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

        public void SerializeFieldData()
        {
            JsonMetadata = JsonConvert.SerializeObject(FieldMetadatas);
        }
    }
}
