using DataProcessor.Dto;
using Microsoft.Extensions.Configuration;

namespace DataProcessor.Services;

using System;
using System.Data.SQLite;

public class DatabaseManager
{
    private readonly string connectionString;


    public DatabaseManager()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var appsettingsPath = Path.Combine(currentDirectory, "..", "..", "..", "..", "appsettings.json");
        
        var configuration = new ConfigurationBuilder().AddJsonFile(appsettingsPath).Build();
        connectionString = configuration["DatabaseConnectionString"];
    }

    public void CreateDatabaseAndTable()
    {
        bool databaseExists = false;

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string checkDatabaseQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='ModuleStates'";
            using (SQLiteCommand checkCommand = new SQLiteCommand(checkDatabaseQuery, connection))
            {
                using (SQLiteDataReader reader = checkCommand.ExecuteReader())
                {
                    databaseExists = reader.HasRows;
                }
            }
        }

        if (!databaseExists)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"CREATE TABLE ModuleStates (
                                        ModuleCategoryID TEXT PRIMARY KEY,
                                        ModuleState TEXT)";
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }


    public void InsertOrUpdateModuleState(ModuleStateDto moduleState)
    {
        bool recordExists = false;
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string checkQuery = "SELECT COUNT(*) FROM ModuleStates WHERE ModuleCategoryID = @ModuleCategoryID";
            using (SQLiteCommand command = new SQLiteCommand(checkQuery, connection))
            {
                command.Parameters.AddWithValue("@ModuleCategoryID", moduleState.ModuleCategoryID);
                recordExists = Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string query;
            if (recordExists)
            {
                query = "UPDATE ModuleStates SET ModuleState = @ModuleState WHERE ModuleCategoryID = @ModuleCategoryID";
            }
            else
            {
                query = "INSERT INTO ModuleStates (ModuleCategoryID, ModuleState) VALUES (@ModuleCategoryID, @ModuleState)";
            }

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ModuleCategoryID", moduleState.ModuleCategoryID);
                command.Parameters.AddWithValue("@ModuleState", moduleState.ModuleState);
                command.ExecuteNonQuery();
            }
        }
    }
}

