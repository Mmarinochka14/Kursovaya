using Newtonsoft.Json;
using Label_Helper.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Label_Helper.View
{
    public partial class Artists : UserControl
    {
        private ArtistVM artistVM = new ArtistVM();

        public Artists()
        {
            InitializeComponent();

            // Установка источника данных для ArtistListView
            ArtistListView.ItemsSource = artistVM.Artists;

            // При запуске программы пытаемся загрузить сохраненные данные
            artistVM.LoadData();
        }

        private void AddArtist_Click(object sender, RoutedEventArgs e)
        {
            // Создание нового объекта Artist с данными из текстовых полей
            Artist newArtist = new Artist
            {
                Nickname = NicknameTextBox.Text,
                Name = NameTextBox.Text,
                Age = int.Parse(AgeTextBox.Text),
                City = CityTextBox.Text,
                Biography = BioTextBox.Text
            };

            // Добавление нового артиста в коллекцию и обновление списка
            artistVM.AddArtist(newArtist);
            ClearTextBoxes();
        }

        private void EditArtist_Click(object sender, RoutedEventArgs e)
        {
            // Заполнение текстовых полей данными выбранного артиста для редактирования
            if (ArtistListView.SelectedItem != null)
            {
                Artist selectedArtist = (Artist)ArtistListView.SelectedItem;
                NicknameTextBox.Text = selectedArtist.Nickname;
                NameTextBox.Text = selectedArtist.Name;
                AgeTextBox.Text = selectedArtist.Age.ToString();
                CityTextBox.Text = selectedArtist.City;
                BioTextBox.Text = selectedArtist.Biography;
            }
        }

        private void DeleteArtist_Click(object sender, RoutedEventArgs e)
        {
            // Удаление выбранного артиста из коллекции и обновление списка
            if (ArtistListView.SelectedItem != null)
            {
                Artist selectedArtist = (Artist)ArtistListView.SelectedItem;
                artistVM.RemoveArtist(selectedArtist);

                // Обновляем источник данных для ArtistListView
                ArtistListView.ItemsSource = null;
                ArtistListView.ItemsSource = artistVM.Artists;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Сохранение изменений данных выбранного артиста и обновление списка
            if (ArtistListView.SelectedItem != null)
            {
                Artist selectedArtist = (Artist)ArtistListView.SelectedItem;
                selectedArtist.Nickname = NicknameTextBox.Text;
                selectedArtist.Name = NameTextBox.Text;
                selectedArtist.Age = int.Parse(AgeTextBox.Text);
                selectedArtist.City = CityTextBox.Text;
                selectedArtist.Biography = BioTextBox.Text;

                // Обновление списка
                ArtistListView.Items.Refresh();
                ClearTextBoxes();
            }
        }

        private void ClearTextBoxes()
        {
            // Очистка текстовых полей
            NicknameTextBox.Clear();
            NameTextBox.Clear();
            AgeTextBox.Clear();
            CityTextBox.Clear();
            BioTextBox.Clear();
        }
    }
}
