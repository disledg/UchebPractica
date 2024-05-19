using CarManagement.Models;
using CarManagement.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для NewCarWindow.xaml
    /// </summary>
    public partial class NewCarWindow : Window
    {
        string imagePath;
        public NewCarWindow()
        {
            InitializeComponent();
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
            if (!string.IsNullOrEmpty(NumberTextBox.Text) &&
                !string.IsNullOrEmpty(AgeTextBox.Text) &&
                !string.IsNullOrEmpty(ModelTextBox.Text) &&
                !string.IsNullOrEmpty(LocationTextBox.Text) &&
                ServiceTextBox.SelectedDate.HasValue)
            {
                if (int.TryParse(AgeTextBox.Text, out int age))
                {
                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                    {
                        try
                        {
                            byte[] photoBytes = File.ReadAllBytes(imagePath);

                            if (photoBytes.Length > 0)
                            {
                                Car newCar = new Car
                                {
                                    Number = NumberTextBox.Text,
                                    Name = ModelTextBox.Text,
                                    Age = age,
                                    Last_service = ServiceTextBox.SelectedDate.Value.ToString("yyyy-MM-dd"),
                                    Location = LocationTextBox.Text,
                                    Photo = photoBytes
                                };

                                sql dbManager = new sql();

                                dbManager.AddCar(newCar);

                                MessageBox.Show("Автомобиль успешно добавлен в базу данных.");

                                // Обновляем список автомобилей после добавления нового
                                CarViewModel viewModel = DataContext as CarViewModel;
                                if (viewModel != null)
                                {
                                    viewModel.LoadCars();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Выберите корректное изображение.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при добавлении автомобиля: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите изображение для автомобиля.");
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректное значение возраста.");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля ввода.");
            }
        }


    }
}
