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
        return $"Server=.;Database=TaskMaster;User Id={username};Password={password};TrustServerCertificate=True;";
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

    public static User? LoadUser(int userId)
    {
        try
        {
            (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

            string connectionString = GetConnectionString(username, password);
            using SqlConnection connection = new(connectionString);
            connection.Open();
            
            string sql = $"""
                SELECT TOP 1 * FROM Users
                WHERE Id = {userId}
            """;

            SqlCommand command = new(sql, connection);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            User user = new(
                (Role)Convert.ToInt32(reader["Role"]),
                reader["Name"].ToString()!,
                reader["LastName"].ToString()!,
                reader["UserName"].ToString()!,
                reader["Password"].ToString()!,
                (Level)Convert.ToInt32(reader["Level"]),
                Convert.ToInt32(reader["Score"])
            );

            connection.Close();

            return user;
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
}