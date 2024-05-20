using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
                Cars = new ObservableCollection<Car>(_sqlInstance.GetAllCars());
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
