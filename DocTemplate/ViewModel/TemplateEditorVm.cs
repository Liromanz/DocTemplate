﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
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

        private string _numerType;
        public string NumerType
        {
            get => _numerType;
            set
            {
                _numerType = value;
                FieldMetadatas.First(x => x.Name == CurrentField).NumerType = value;
                OnPropertyChanged();
            }
        }


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

        private string _dateType;

        public string DateType
        {
            get => _dateType;
            set
            {
                _dateType = value;
                FieldMetadatas.First(x => x.Name == CurrentField).DateMask = value;
                OnPropertyChanged();
            }
        }

        private bool _isImage;
        public bool IsImage
        {
            get => _isImage;
            set
            {
                _isImage = value; 
                OnPropertyChanged();
            }
        }

        private string _fileTypes="";

        public string FileTypes
        {
            get => _fileTypes;
            set
            {
                _fileTypes = value;
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
            if (FileTypes.EndsWith('|'))
                FileTypes = FileTypes.Substring(0, FileTypes.Length - 1);
            FieldMetadatas.First(x => x.Name == CurrentField).FileTypes = FileTypes;
            JsonMetadata = JsonConvert.SerializeObject(FieldMetadatas);
        }
    }
}
