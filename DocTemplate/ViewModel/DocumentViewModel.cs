using System.Windows;
using DocTemplate.Helpers;
using DocTemplate.View;

namespace DocTemplate.ViewModel
{
    public class DocumentViewModel
    {
        #region Команды
        public BindableCommand ReturnCommand { get; set; }
        #endregion

        #region Переменные

        #endregion

        public DocumentViewModel()
        {
            ReturnCommand = new BindableCommand(x => ReturnToWindow());
        }

        private void ReturnToWindow()
        {
            var window = Application.Current.MainWindow;
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            window.Close();
        }
    }
}
