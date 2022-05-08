using DocTemplate.Global.Models;
using DocTemplate.Helpers;
using DocTemplate.View;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using Forms = System.Windows.Forms;

namespace DocTemplate.ViewModel
{
    public class DocumentViewModel
    {
        #region Команды
        public BindableCommand ReturnCommand { get; set; }
        public BindableCommand ExportCommand { get; set; }
        #endregion

        #region Переменные
        public DocumentWindow ThisWindow { get; set; }
        public List<FieldMetadata> FieldMetadatas { get; set; } = new List<FieldMetadata>();

        #endregion

        public DocumentViewModel()
        {
            ReturnCommand = new BindableCommand(x => ReturnToWindow());
            ExportCommand = new BindableCommand(x => ExportDocument());
        }

        private void ReturnToWindow()
        {
            var window = Application.Current.MainWindow;
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            window.Close();
        }

        private void ExportDocument()
        {
            Forms.SaveFileDialog fileDialog = new Forms.SaveFileDialog();
            fileDialog.Filter = "Word файл|*.docx|PDF файл|*.pdf|Текстовый файл|*.txt|RTF файл|*.rtf";
            if (fileDialog.ShowDialog() == Forms.DialogResult.OK)
            {
                using (FileStream fs = new FileStream(Path.GetTempPath() + "filetosave.rtf", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    TextRange textRange = new TextRange(ThisWindow.flowDocument.ContentStart, ThisWindow.flowDocument.ContentEnd);
                    textRange.Save(fs, DataFormats.Rtf);
                }

                object fileFormat;
                switch (fileDialog.FileName.Substring(fileDialog.FileName.LastIndexOf('.') + 1))
                {
                    case "docx":
                        fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault;
                        break;
                    case "pdf":
                        fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
                        break;
                    case "txt":
                        fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatText;
                        break;
                    default:
                        fileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatRTF;
                        break;
                }

                var wordApp = new Microsoft.Office.Interop.Word.Application();
                var currentDoc = wordApp.Documents.Open(Path.GetTempPath() + "filetosave.rtf");
                currentDoc.SaveAs(fileDialog.FileName, fileFormat);
                currentDoc.Close();

                MessageBox.Show("Файл сохранен!");
                File.Delete(Path.GetTempPath() + "filetosave.rtf");
            }
        }
    }
}
