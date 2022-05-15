using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DocTemplate.Helpers
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Метод для обновления значения параметра
        /// </summary>
        /// <param name="name">Имя параметра</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
