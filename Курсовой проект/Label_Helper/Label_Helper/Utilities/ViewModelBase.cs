using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Label_Helper.Utilities
{
    // Класс ViewModelBase реализует интерфейс INotifyPropertyChanged, предоставляя базовую функциональность для уведомления об изменении свойств в классах ViewModel.
    public class ViewModelBase : INotifyPropertyChanged
    {
        // Событие, которое возникает при изменении значения свойства
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод, вызываемый для уведомления об изменении значения свойства
        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            // Проверка, что кто-то подписан на событие, прежде чем его вызвать
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Второй вариант метода для уведомления об изменении значения свойства, где имя свойства передается явно
        public event PropertyChangedEventHandler PropertyChanged1;
        public void OnPropertyChanged1(string propertyName)
        {
            PropertyChanged1?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
