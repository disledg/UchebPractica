using CarManagement.Models;
using NUnit.Framework;
using Practica;
using System.Collections.Generic;

namespace CarManagement.Tests
{
    [TestFixture]
    public class CarManagerTests
    {
        private sql carManager;

        [SetUp]
        public void Setup()
        {
            // Инициализация объекта carManager перед каждым тестом
            carManager = new sql();
        }

        [Test]
        public void GetAllCars_Returns_CorrectCount()
        {
            // Arrange
            // Подготовка данных (заглушка)
            List<Car> expectedCars = new List<Car>
            {
                new Car { Id = 1, Number = "ABC123", Name = "Toyota", Age = 5, Last_service = "2022-01-01", Location = "City", Photo = null },
                new Car { Id = 2, Number = "XYZ789", Name = "Honda", Age = 3, Last_service = "2022-02-02", Location = "Suburb", Photo = null }
                // Добавьте другие машины по мере необходимости
            };

            // Act
            // Вызов метода GetAllCars()
            List<Car> actualCars = carManager.GetAllCars();

            // Assert
            // Проверка количества возвращаемых машин
            Assert.AreEqual(expectedCars.Count, actualCars.Count);
        }

        // Другие тесты для проверки содержимого, обработки ошибок и т. д.
    }
}
