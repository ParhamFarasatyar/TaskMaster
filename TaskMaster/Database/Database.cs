using System.Text.Json;
namespace DataBase;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;
using UserModel;
using TaskMaster.DataModel;

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
    public static void SaveQuestion(Question question)
    {
        string username = SystemValidation.System.GetSqlAuthenticationInfo().Item1;
        string password = SystemValidation.System.GetSqlAuthenticationInfo().Item2;
        string ConnectionString = GetConnectionString(username, password);
        SqlConnection connection = new(ConnectionString);
        connection.Open();
        string sqlcommand = """
        INSERT INTO Questions
        VALUES(@Description, @Grade, @Difficulty, @CreatedAt, @UpdatedAt)
        """;
        SqlCommand command = new(sqlcommand);
        command.Parameters.AddWithValue("@Description" , question.Description);
        command.Parameters.AddWithValue("@Grade" , question.Grade);
        command.Parameters.AddWithValue("@Difficulty" , question.Difficulty);
        command.Parameters.AddWithValue("@CreatedAt" , question.CreatedAt);
        command.Parameters.AddWithValue("@UpdatedAt" , question.UpdatedAt);
        int sqlDataReader = command.ExecuteNonQuery();
        Console.WriteLine($"{sqlDataReader} Rows affected");
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