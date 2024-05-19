using System;

namespace CarManagement.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public int Age { get; set; } // Возраст автомобиля в годах
        public string Last_service { get; set; }
        public string Location { get; set; }
        public byte[] Photo { get; set; }
    }
}
