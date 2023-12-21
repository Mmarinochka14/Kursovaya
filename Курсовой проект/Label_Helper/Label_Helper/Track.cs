using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Label_Helper
{
    // Класс, представляющий трек
    [JsonObject]
    public class Track
    {
        // Название трека
        public string Title { get; set; }

        // Название альбома, которому принадлежит трек
        public string Album { get; set; }

        // Флаг, указывающий, выбран ли трек
        public bool IsSelected { get; set; }
    }
}
