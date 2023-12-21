using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Label_Helper
{
    // Класс, представляющий артиста
    public class Artist
    {
        // Никнейм артиста
        public string Nickname { get; set; }

        // Имя артиста
        public string Name { get; set; }

        // Возраст артиста
        public int Age { get; set; }

        // Город, в котором находится артист
        public string City { get; set; }

        // Биография артиста
        public string Biography { get; set; }

        // Коллекция альбомов артиста
        public ObservableCollection<Album> Albums { get; set; } = new ObservableCollection<Album>();
    }
}
