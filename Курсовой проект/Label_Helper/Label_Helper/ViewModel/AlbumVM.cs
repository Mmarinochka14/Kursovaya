using Newtonsoft.Json;
using Label_Helper.Utilities;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace Label_Helper.ViewModel
{
    public class AlbumVM : ViewModelBase
    {
        // Коллекции для хранения данных об альбомах, артистах
        private ObservableCollection<Album> albums;
        private ObservableCollection<Artist> artists;

        // Свойства для привязки данных к пользовательскому интерфейсу
        public ObservableCollection<Album> Albums
        {
            get { return albums; }
            set
            {
                if (albums != value)
                {
                    albums = value;
                    OnPropertyChanged(nameof(Albums));
                }
            }
        }

        // Выбранный альбом
        private Album _selectedAlbum;
        public Album SelectedAlbum
        {
            get { return _selectedAlbum; }
            set
            {
                if (_selectedAlbum != value)
                {
                    _selectedAlbum = value;
                    OnPropertyChanged(nameof(SelectedAlbum));
                }
            }
        }

        // Выбранный трек
        private Track _selectedTrack;
        public Track SelectedTrack
        {
            get { return _selectedTrack; }
            set
            {
                if (_selectedTrack != value)
                {
                    _selectedTrack = value;
                    OnPropertyChanged(nameof(SelectedTrack));
                }
            }
        }

        // Коллекция артистов
        public ObservableCollection<Artist> Artists
        {
            get { return artists; }
            set
            {
                if (artists != value)
                {
                    artists = value;
                    OnPropertyChanged(nameof(Artists));
                }
            }
        }

        // Выбранный артист
        private Artist selectedArtist;
        public Artist SelectedArtist
        {
            get { return selectedArtist; }
            set
            {
                if (selectedArtist != value)
                {
                    selectedArtist = value;
                    OnPropertyChanged(nameof(SelectedArtist));
                    LoadAlbums();
                }
            }
        }


        public AlbumVM()
        {
            LoadData();
        }

        // Метод загрузки данных из файлов JSON
        public void LoadData()
        {
            try
            {
                // Загрузка данных об артистах
                if (File.Exists("Data/artists.json"))
                {
                    string jsonData = File.ReadAllText("Data/artists.json");
                    Artists = JsonConvert.DeserializeObject<ObservableCollection<Artist>>(jsonData);
                }
                else
                {
                    SaveData(); // Если файл отсутствует, создаем его и сохраняем данные
                }

                // Загрузка данных об альбомах
                if (File.Exists("Data/albums.json"))
                {
                    string jsonData = File.ReadAllText("Data/albums.json");
                    Albums = JsonConvert.DeserializeObject<ObservableCollection<Album>>(jsonData);
                }
                else
                {
                    SaveData(); // Если файл отсутствует, создаем его и сохраняем данные
                }
            }
            catch (Exception ex)
            {
                // В случае ошибки выводим сообщение об ошибке
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод загрузки альбомов для выбранного артиста
        private void LoadAlbums()
        {
            if (SelectedArtist != null)
            {
                var artistAlbums = Albums.Where(album => album.Artist == SelectedArtist.Nickname).ToList();
                Albums = new ObservableCollection<Album>(artistAlbums);
            }
        }

        // Метод сохранения данных в файлы JSON
        private void SaveData()
        {
            try
            {
                // Проверяем наличие каталога, и создаем его, если он отсутствует
                string dataDirectory = "Data";
                if (!Directory.Exists(dataDirectory))
                {
                    Directory.CreateDirectory(dataDirectory);
                }

                // Сериализация данных об артистах
                string jsonData = JsonConvert.SerializeObject(Artists, Formatting.Indented);
                File.WriteAllText(Path.Combine(dataDirectory, "artists.json"), jsonData);

                // Сериализация данных об альбомах
                jsonData = JsonConvert.SerializeObject(Albums, Formatting.Indented);
                File.WriteAllText(Path.Combine(dataDirectory, "albums.json"), jsonData);

                // Уведомляем об изменениях в данных
                OnPropertyChanged(nameof(Artists));
                OnPropertyChanged(nameof(Albums));
            }
            catch (Exception ex)
            {
                // В случае ошибки выводим сообщение об ошибке
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод для добавления нового альбома
        public void AddAlbum(string albumName, Artist artist)
        {
            Album newAlbum = new Album
            {
                Name = albumName,
                Artist = artist.Nickname,
                Tracks = new ObservableCollection<Track>()
            };

            // Добавляем новый альбом в коллекцию
            Albums.Add(newAlbum);

            // Сохраняем данные
            SaveData();
        }

        // Метод для удаления альбома
        public void DeleteAlbum(Album album)
        {
            if (album != null)
            {
                Albums.Remove(album);
                SaveData();
            }
        }

        // Метод для добавления нового трека к альбому
        public void AddTrack(string trackTitle, Album album)
        {
            if (album != null)
            {
                Track newTrack = new Track
                {
                    Title = trackTitle
                };

                // Добавляем новый трек к выбранному альбому
                album.Tracks.Add(newTrack);
                SaveData();
            }
        }

        // Метод для удаления трека из альбома
        public void DeleteTrack(Album album, Track track)
        {
            if (album != null && track != null)
            {
                // Удаляем трек из выбранного альбома
                album.Tracks.Remove(track);
                SaveData();
            }
        }

        // Метод для изменения названия альбома
        public void EditAlbum(Album album, string newAlbumName)
        {
            if (album != null)
            {
                // Изменяем название альбома
                album.Name = newAlbumName;
                SaveData();
            }
        }

        // Метод для изменения названия трека
        public void EditTrack(Album album, Track track, string newTrackTitle)
        {
            if (album != null && track != null)
            {
                // Изменяем название трека
                track.Title = newTrackTitle;
                SaveData();
            }
        }
    }
}
