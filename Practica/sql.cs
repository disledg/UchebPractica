using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CarManagement.Models;
using System.Data;
namespace Practica
{
    internal class sql
    {
        string tableName = "Car";

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-8UQBG00;Initial Catalog=Ucheba;Integrated Security=True");
        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }
        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }

        }
        public SqlConnection getConnection()
        {
            return sqlConnection;
        }
        public List<Car> GetAllCars()
        {
            List<Car> cars = new List<Car>();
            string query = $"SELECT * FROM {tableName}";

            try
            {
                openConnection();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Car car = new Car
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Number = reader["Number"].ToString(),
                                Name = reader["Name"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                Last_service = reader["Last_service"].ToString(),
                                Location = reader["Location"].ToString(),
                                Photo = reader["Photo"] as byte[]
                            };

                            cars.Add(car);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine($"Ошибка при извлечении данных: {ex.Message}");
            }
            finally
            {
                closeConnection();
            }

            return cars;
        }
        public void DeleteCar(int id)
        {
            string query = $"DELETE FROM {tableName} WHERE Id = @Id";

            try
            {
                openConnection();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine($"Ошибка при удалении автомобиля: {ex.Message}");
            }
            finally
            {
                closeConnection();
            }
        }



        public void AddCar(Car car)
        {
            string query = $"INSERT INTO {tableName} (Number, Name, Age, Last_service, Location, Photo) " +
                           "VALUES (@Number, @Name, @Age, @Last_service, @Location, @Photo)";

            try
            {
                openConnection();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Number", car.Number);
                    command.Parameters.AddWithValue("@Name", car.Name);
                    command.Parameters.AddWithValue("@Age", car.Age);
                    command.Parameters.AddWithValue("@Last_service", car.Last_service);
                    command.Parameters.AddWithValue("@Location", car.Location);
                    command.Parameters.AddWithValue("@Photo", car.Photo);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine($"Ошибка при добавлении автомобиля: {ex.Message}");
            }
            finally
            {
                closeConnection();
            }
        }
        public List<Car> SearchCars(string searchTerm)
        {
            List<Car> cars = new List<Car>();
            string query = $"SELECT * FROM {tableName} WHERE Name LIKE @SearchTerm";

            try
            {
                openConnection();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Car car = new Car
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Number = reader["Number"].ToString(),
                                Name = reader["Name"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                Last_service = reader["Last_service"].ToString(),
                                Location = reader["Location"].ToString(),
                                Photo = reader["Photo"] as byte[]
                            };

                            cars.Add(car);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine($"Ошибка при поиске автомобилей: {ex.Message}");
            }
            finally
            {
                closeConnection();
            }

            return cars;
        }

    }
}
