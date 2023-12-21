using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace Label_Helper
{
    // Класс, представляющий альбом
    [JsonObject]
    public class Album
    {
        // Название альбома
        public string Name { get; set; }

        // Коллекция треков альбома
        public ObservableCollection<Track> Tracks { get; set; } = new ObservableCollection<Track>();

        // Ссылка на артиста, к которому принадлежит альбом
        public string Artist { get; set; }

        // Флаг, указывающий, выбран ли альбом
        public bool IsSelected { get; set; }
    }
}
