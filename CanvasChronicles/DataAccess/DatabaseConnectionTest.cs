using MySql.Data.MySqlClient;
using System;

namespace CanvasChronicles.DataAccess; 

public class DatabaseConnectionTest {
    private string _connectionString;

    public DatabaseConnectionTest(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool IsDatabaseConnected()
    {
        try
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connection successful.");
                    return true;
                }
                Console.WriteLine("Connection failed.");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
}