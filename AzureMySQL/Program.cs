using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace AzureMySQL
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=<server name>.mysql.database.azure.com;uid=<userid>;pwd=<password>;database=employee";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    InsertData(connection);
                    SelectData(connection);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("\npress any key to continue...");
            Console.Read();
        }
        private static void InsertData(MySqlConnection connection)
        {
            string Query = String.Format("insert into employee.employees(EmpID, EmpName) values('{0}','{1}');", Guid.NewGuid().ToString(), "A Name");

            MySqlCommand cmd = new MySqlCommand(Query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine("\nRecord has been inserted.");
            }
        }
        private static void SelectData(MySqlConnection connection)
        {
            MySqlCommand cmd = new MySqlCommand("employees", connection);
            cmd.CommandType = CommandType.TableDirect;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string id = Convert.ToString(reader[0]);
                    string name = Convert.ToString(reader[1]);

                    Console.WriteLine(String.Format("Id: {0} and Name: {1}", id, name));
                }
            }
        }
    }
}
