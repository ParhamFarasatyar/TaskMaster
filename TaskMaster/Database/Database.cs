using System.Text.Json;
namespace DataBase;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;
using UserModel;

public enum DataType { Users, Questions, Answers }

public static class Database
{
    private static string GetConnectionString(string username, string password)
    {
        return $"Server=192.168.1.138,1433;Database=TaskMaster;User Id={username};Password={password};TrustServerCertificate=True;";
    }
    private static readonly JsonSerializerOptions options = new()
    {
        WriteIndented = true,
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };
    private static string GetPath(DataType type)
    {
        string dataFolder = Path.Combine(
            AppContext.BaseDirectory,
            "Data"
        );

        Directory.CreateDirectory(dataFolder);

        return Path.Combine(
            dataFolder,
            $"{type}.json"
        );
    }
    
    public static void SaveUser(User data)
    {
        try
        {
            (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

            string connectionString = GetConnectionString(username, password);
            using SqlConnection connection = new(connectionString);
            connection.Open();
            
            string sql = """
                INSERT INTO Users
                VALUES(@Name, @LastName, @UserName, @Password, @Level, @Score, @Role)
            """;

            SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@Name", data.Name);
            command.Parameters.AddWithValue("@LastName", data.LastName);
            command.Parameters.AddWithValue("@UserName", data.UserName);
            command.Parameters.AddWithValue("@Password", data.Password);
            command.Parameters.AddWithValue("@Level", data.Level);
            command.Parameters.AddWithValue("@Score", data.Score);
            command.Parameters.AddWithValue("@Role", data.Role);

            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (InvalidOperationException e)
        {
            SystemValidation.System.PrintColorizeMessage(e.Message, ConsoleColor.Red);
            return;
        }
    }

    public static List<string>? LoadUser()
    {
        try
        {
            (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

            string connectionString = GetConnectionString(username, password);
            using SqlConnection connection = new(connectionString);
            connection.Open();
            
            string sql = """
                SELECT UserName FROM Users
            """;

            SqlCommand command = new(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            List<string> users = new List<string>();
            while (reader.Read())
            {
                users.Add(reader["UserName"].ToString()!);
            }

            connection.Close();

            return users;
        }
        catch (InvalidOperationException e)
        {
            SystemValidation.System.PrintColorizeMessage(e.Message, ConsoleColor.Red);
            return null;
        }
    }
    
    public static List<T> Load<T>(DataType type)
    {
        string path = GetPath(type);
        
        if (!File.Exists(path))
        {
            return new List<T>();
        }
        
        string json = File.ReadAllText(path);
        
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<T>();
        }
        
        return JsonSerializer.Deserialize<List<T>>(json, options)
               ?? new List<T>();
    }
    
    public static void Update<T>(
        List<T> data,
        DataType type)
    {
        string path = GetPath(type);
        
        string json = JsonSerializer.Serialize(
            data,
            options
        );
        
        File.WriteAllText(
            path,
            json
        );
    }

    public static void UpdateUser(int userId, string field, string data)
    {
        (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

        string connectionString = GetConnectionString(username, password);
        using SqlConnection connection = new(connectionString);
        connection.Open();

        string sql;
        if (field == "Name")
        {
            sql = $"""
                UPDATE Users
                SET Name = @Name
                WHERE Id = @UserId
            """;

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Name", data);
            command.Parameters.AddWithValue("@UserId", userId);
            int rowAffected = command.ExecuteNonQuery();
        }
        else if (field == "LastName")
        {
            sql = $"""
                UPDATE Users
                SET LastName = @LastName
                WHERE Id = @UserId
            """;

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@LastName", data);
            command.Parameters.AddWithValue("@UserId", userId);
            int rowAffected = command.ExecuteNonQuery();
        }
        else if (field == "UserName")
        {
            sql = $"""
                UPDATE Users
                SET UserName = @UserName
                WHERE Id = @UserId
            """;

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@UserName", data);
            command.Parameters.AddWithValue("@UserId", userId);
            int rowAffected = command.ExecuteNonQuery();
        }
        else if (field == "Password")
        {
            sql = $"""
                UPDATE Users
                SET Password = @Password
                WHERE Id = @UserId
            """;

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Password", data);
            command.Parameters.AddWithValue("@UserId", userId);
            int rowAffected = command.ExecuteNonQuery();
        }
        else
        {
            SystemValidation.System.PrintColorizeMessage(
                "Invalid field selected!",
                ConsoleColor.Red
            );
            return;
        }
    }
    public static void DeleteUser(int userId)
    {
        (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

        string connectionString = GetConnectionString(username, password);
        using SqlConnection connection = new(connectionString);
        connection.Open();

        string sql = $"""
            DELETE Users
            WHERE Id = @UserId
        """;
        using SqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@UserId", userId);
        int rowAffected = command.ExecuteNonQuery();
    }
}