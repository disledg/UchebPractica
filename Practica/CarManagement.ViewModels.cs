using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using CarManagement.Models;
using Practica;

namespace CarManagement.ViewModels
{
    public class CarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Car> _cars;
        private sql _sqlInstance;

        public ObservableCollection<Car> Cars
        {
            get { return _cars; }
            set
            {
                _cars = value;
                OnPropertyChanged("Cars");
            }
        }
        public void SearchCars(string category, string keyword)
        {
            ObservableCollection<Car> result = new ObservableCollection<Car>();

            switch (category)
            {
                case "Номер":
                    result = new ObservableCollection<Car>(_cars.Where(car => car.Number.ToLower().Contains(keyword.ToLower())));
                    break;
                case "Модель и марка":
                    result = new ObservableCollection<Car>(_cars.Where(car => car.Name.ToLower().Contains(keyword.ToLower())));
                    break;
                case "Местоположение":
                    result = new ObservableCollection<Car>(_cars.Where(car => car.Location.ToLower().Contains(keyword.ToLower())));
                    break;
                case "Последнее обслуживание":
                    result = new ObservableCollection<Car>(_cars.Where(car => car.Last_service.ToLower().Contains(keyword.ToLower())));
                    break;
                case "Возраст":
                    if (int.TryParse(keyword, out int age))
                    {
                        result = new ObservableCollection<Car>(_cars.Where(car => car.Age == age));
                    }
                    else
                    {
                        MessageBox.Show("Введите корректное значение для возраста.");
                    }
                    break;
            }

            Cars = result;
        }
        public CarViewModel()
        {
            try
            {
                _sqlInstance = new sql(); // Инициализация _sqlInstance
                LoadCars();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка при инициализации CarViewModel: {ex.Message}");
            }
        }
        public void Refresh()
        {
            // Создаем экземпляр класса sql
            sql sqlInstance = new sql();
            
            // Получаем обновленные данные из базы данных
            var updatedCars = new ObservableCollection<Car>(sqlInstance.GetAllCars());

            // Присваиваем обновленные данные свойству Cars
            Cars = updatedCars;
        }
        public void LoadCars()
        {
            try
            {
                sql sqlInstance = new sql();

                // Загружаем данные из базы данных
                Cars = new ObservableCollection<Car>(sqlInstance.GetAllCars());
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка при загрузке автомобилей: {ex.Message}");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
