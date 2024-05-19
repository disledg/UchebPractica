using CarManagement.Models;
using CarManagement.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practica
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CarViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewCarWindow newWindow = new NewCarWindow();
            newWindow.Owner = this;

            // Блокируем ввод для текущего окна
            this.IsEnabled = false;

            // Показываем новое окно модально (блокирует ввод для предыдущего окна)
            newWindow.ShowDialog();

            // После закрытия нового окна делаем текущее окно снова активным
            this.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CarViewModel viewModel = DataContext as CarViewModel;

            // Проверяем, что viewModel не равен null
            if (viewModel != null)
            {
                // Вызываем метод для обновления списка автомобилей
                viewModel.Refresh();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный элемент из ListView
            Car selectedCar = listViewCars.SelectedItem as Car;

            // Проверяем, что элемент был выбран
            if (selectedCar != null)
            {
                // Удаляем выбранный элемент из списка автомобилей
                CarViewModel viewModel = DataContext as CarViewModel;
                viewModel.Cars.Remove(selectedCar);

                // Удаляем выбранный автомобиль из базы данных
                sql sqlInstance = new sql();
                sqlInstance.DeleteCar(selectedCar.Id);
            }
            else
            {
                MessageBox.Show("Выберите автомобиль для удаления.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CarViewModel viewModel = DataContext as CarViewModel;
            if (viewModel != null)
            {
                string searchTerm = SearchTextBox.Text;
                viewModel.SearchCars(searchTerm);
            }
        }
    }
}
