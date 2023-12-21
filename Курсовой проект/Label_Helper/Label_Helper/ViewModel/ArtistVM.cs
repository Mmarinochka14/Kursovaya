using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Label_Helper.ViewModel
{
    public class ArtistVM
    {
        // Коллекция для хранения данных об артистах
        private ObservableCollection<Artist> artists = new ObservableCollection<Artist>();

        // Свойство для привязки данных к пользовательскому интерфейсу
        public ObservableCollection<Artist> Artists
        {
            get { return artists; }
            set
            {
                if (artists != value)
                {
                    artists = value;
                }
            }
        }

        public ArtistVM()
        {
            LoadData();
        }

        // Метод для добавления нового артиста
        public void AddArtist(Artist artist)
        {
            Debug.WriteLine("public void AddArtist(Artist artist)");

            // Выводим имена всех артистов в отладочное окно
            foreach (var Artist in Artists)
            {
                Debug.WriteLine(Artist.Name);
            }

            // Добавляем нового артиста в коллекцию
            artists.Add(artist);
            SaveData();
        }

        // Метод для удаления артиста
        public void RemoveArtist(Artist artist)
        {
            // Удаляем артиста из коллекции
            artists.Remove(artist);
            SaveData();
        }

        // Метод для добавления нового альбома для артиста
        public void AddAlbum(Artist artist, Album album)
        {
            // Добавляем альбом к коллекции альбомов артиста
            artist.Albums.Add(album);
            SaveData();
        }

        // Метод для удаления альбома артиста
        public void RemoveAlbum(Artist artist, Album album)
        {
            // Удаляем альбом из коллекции альбомов артиста
            artist.Albums.Remove(album);
            SaveData();
        }

        // Метод для добавления нового трека к альбому
        public void AddTrack(Album album, Track track)
        {
            // Добавляем трек к коллекции треков альбома
            album.Tracks.Add(track);
            SaveData();
        }

        // Метод для удаления трека из альбома
        public void RemoveTrack(Album album, Track track)
        {
            // Удаляем трек из коллекции треков альбома
            album.Tracks.Remove(track);
            SaveData();
        }

        // Метод для загрузки данных из файла JSON
        public void LoadData()
        {
            try
            {
                // Загружаем данные об артистах из файла "artists.json"
                if (File.Exists("Data/artists.json"))
                {
                    string jsonData = File.ReadAllText("Data/artists.json");
                    artists = JsonConvert.DeserializeObject<ObservableCollection<Artist>>(jsonData);
                }
                else
                {
                    // Если файл не существует, создаем его
                    SaveData();
                }
            }
            catch (Exception ex)
            {
                // В случае ошибки выводим сообщение об ошибке
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод для сохранения данных в файл JSON
        public void SaveData()
        {
            try
            {
                // Проверяем наличие каталога, и создаем его, если он отсутствует
                string dataDirectory = "Data";
                if (!Directory.Exists(dataDirectory))
                {
                    Directory.CreateDirectory(dataDirectory);
                }

                // Сериализация данных об артистах в формат JSON
                string jsonData = JsonConvert.SerializeObject(artists, Formatting.Indented);
                File.WriteAllText(Path.Combine(dataDirectory, "artists.json"), jsonData);
            }
            catch (Exception ex)
            {
                // В случае ошибки выводим сообщение об ошибке
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
