using CarManagement.Models;
using CarManagement.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Practica
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Car _originalCar;
        string imagePath;
        public EditWindow(Car car)
        {
            InitializeComponent();
            _originalCar = car;
            // Заполняем поля формы данными из выбранного автомобиля
            NumberTextBox.Text = car.Number;
            ModelTextBox.Text = car.Name;
            AgeTextBox.Text = car.Age.ToString();
            ServiceTextBox.SelectedDate = DateTime.Parse(car.Last_service); // Преобразуем строку в дату
            LocationTextBox.Text = car.Location;

            if (car.Photo != null)
            {
                using (MemoryStream memoryStream = new MemoryStream(car.Photo))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = memoryStream;
                    bitmap.EndInit();
                    imageControl.Source = bitmap;
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
                BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                imageControl.Source = bitmap;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            byte[] photoBytes = _originalCar.Photo;

            if (!string.IsNullOrEmpty(imagePath))
            {
                photoBytes = File.ReadAllBytes(imagePath);
            }

            Car updatedCar = new Car
            {
                Id = _originalCar.Id, // используйте оригинальный ID
                Number = NumberTextBox.Text,
                Name = ModelTextBox.Text,
                Age = int.Parse(AgeTextBox.Text),
                Last_service = ServiceTextBox.SelectedDate?.ToString("yyyy-MM-dd"),
                Location = LocationTextBox.Text,
                Photo = photoBytes // Добавление нового фото
            };

            sql dbManager = new sql();
            dbManager.UpdateCar(updatedCar);

            MessageBox.Show("Автомобиль успешно обновлен.");
            this.Close();
        }

    }
}
