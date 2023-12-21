using System.Windows;
using Label_Helper.ViewModel;
using System.Windows.Controls;

namespace Label_Helper.View
{
    // Класс представления для страницы "Релизы"
    public partial class Albums : UserControl
    {
        // Экземпляр ViewModel для работы с данными
        private AlbumVM albumVM = new AlbumVM();

        public Albums()
        {
            InitializeComponent();
            DataContext = albumVM;  // Установка контекста данных
            albumVM.LoadData();     // Загрузка данных при инициализации страницы
        }
        private void AddAlbum_Click(object sender, RoutedEventArgs e)
        {
            // Получение названия альбома и выбранного артиста из соответствующих элементов управления
            string albumName = AlbumNameTextBox.Text;
            Artist selectedArtist = (Artist)ChooseArtists.SelectedItem;

            // Вызов метода ViewModel для добавления альбома
            albumVM.AddAlbum(albumName, selectedArtist);

            // Очистка текстовых полей после добавления
            ClearTextBoxes();
        }
        private void DeleteAlbum_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранного альбома из списка
            Album selectedAlbum = (Album)AlbumListView.SelectedItem;

            // Вызов метода ViewModel для удаления альбома
            albumVM.DeleteAlbum(selectedAlbum);

            // Очистка текстовых полей после удаления
            ClearTextBoxes();
        }
        private void AddTrack_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранного альбома и названия трека из соответствующих элементов управления
            Album selectedAlbum = albumVM.SelectedAlbum;
            string trackTitle = TrackTitleTextBox.Text;

            // Вызов метода ViewModel для добавления трека
            albumVM.AddTrack(trackTitle, selectedAlbum);

            // Очистка текстовых полей после добавления
            ClearTextBoxes();
        }

        private void DeleteTrack_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранного альбома и трека из списков
            Album selectedAlbum = albumVM.SelectedAlbum;
            Track selectedTrack = albumVM.SelectedTrack;

            // Вызов метода ViewModel для удаления трека
            albumVM.DeleteTrack(selectedAlbum, selectedTrack);

            // Очистка текстовых полей после удаления
            ClearTextBoxes();
        }

        // Обработчик события изменения выбранного элемента в списке альбомов
        private void AlbumListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Установка выбранного альбома в ViewModel
            albumVM.SelectedAlbum = (Album)AlbumListView.SelectedItem;
        }

        // Обработчик события изменения выбранного элемента в списке треков
        private void TrackListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Установка выбранного трека в ViewModel
            albumVM.SelectedTrack = (Track)TrackListView.SelectedItem;
        }

        private void EditTrack_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранного альбома и трека, а также нового названия трека
            Album selectedAlbum = albumVM.SelectedAlbum;
            Track selectedTrack = albumVM.SelectedTrack;
            string newTrackTitle = TrackTitleTextBox.Text;

            // Вызов метода ViewModel для редактирования трека
            albumVM.EditTrack(selectedAlbum, selectedTrack, newTrackTitle);

            // Очистка текстовых полей после редактирования
            ClearTextBoxes();
        }

        private void EditAlbum_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранного альбома и нового названия альбома
            Album selectedAlbum = (Album)AlbumListView.SelectedItem;
            string newAlbumName = AlbumNameTextBox.Text;

            // Вызов метода ViewModel для редактирования альбома
            albumVM.EditAlbum(selectedAlbum, newAlbumName);

            // Очистка текстовых полей после редактирования
            ClearTextBoxes();
        }

        // Метод для очистки текстовых полей
        private void ClearTextBoxes()
        {
            AlbumNameTextBox.Clear();
            TrackTitleTextBox.Clear();
        }
    }
}
