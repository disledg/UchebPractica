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
            // ������������� ������� carManager ����� ������ ������
            carManager = new sql();
        }

        [Test]
        public void GetAllCars_Returns_CorrectCount()
        {
            // Arrange
            // ���������� ������ (��������)
            List<Car> expectedCars = new List<Car>
            {
                new Car { Id = 1, Number = "ABC123", Name = "Toyota", Age = 5, Last_service = "2022-01-01", Location = "City", Photo = null },
                new Car { Id = 2, Number = "XYZ789", Name = "Honda", Age = 3, Last_service = "2022-02-02", Location = "Suburb", Photo = null }
                // �������� ������ ������ �� ���� �������������
            };

            // Act
            // ����� ������ GetAllCars()
            List<Car> actualCars = carManager.GetAllCars();

            // Assert
            // �������� ���������� ������������ �����
            Assert.AreEqual(expectedCars.Count, actualCars.Count);
        }

        // ������ ����� ��� �������� �����������, ��������� ������ � �. �.
    }
}
