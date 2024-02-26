using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using CanvasChronicles.Models;

namespace CanvasChronicles.DataAccess; 

public class DataQueryTest {
    private readonly string _connectionString;
    
    public DataQueryTest(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<User> GetAllUsers()
    {
        var users = new List<User>();
        try
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Users";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                Id = reader.GetString("Id"),
                                UserName = reader.GetString("UserName"),
                                Email = reader.GetString("Email"),
                                PasswordHash = reader.GetString("PasswordHash"),
                                CreatedAt = reader.GetDateTime("CreatedAt"),
                                LastModified = reader.GetDateTime("LastModified")
                            };
                            users.Add(user);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        return users;
    }
}