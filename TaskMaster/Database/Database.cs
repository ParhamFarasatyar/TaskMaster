using System.IO.Pipes;
using System.Text.Json;
using TaskMaster.DataModel;

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

    public static User? LoadUser(int userId)
    {
        try
        {
            (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

            string connectionString = GetConnectionString(username, password);
            using SqlConnection connection = new(connectionString);
            connection.Open();
            
            string sql = """
                SELECT TOP 1 * FROM Users
                WHERE Id = @UserId
            """;

            SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@UserId", userId);

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



    public static void SaveAnswer(Answer answer)
    {
        (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

        string connectionString = GetConnectionString(username, password);

        using SqlConnection connection = new SqlConnection(connectionString);
        
        connection.Open();
                                                                // enter table
        string sql = """
                     INSERT INTO (Table)
                     VALUES (@userName, @approvalStatus, @creadDate, @code, @grade, @descriptoin)
                     """;

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@userName", answer.UserName);
        command.Parameters.AddWithValue("@approvalStatus", answer.ApprovalStatus);
        command.Parameters.AddWithValue("@createdDate", answer.CreatedDate);
        command.Parameters.AddWithValue("@code", answer.Code);
        command.Parameters.AddWithValue("@grade", answer.Grade);
        command.Parameters.AddWithValue("@description", answer.Description);

        command.ExecuteNonQuery();

        command.Clone();
    }



    public static void LoadAnswer()
    {
        (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

        string connectionString = GetConnectionString(username, password);

        using SqlConnection connection = new SqlConnection(connectionString);
        
        connection.Open();
        
                                                    // enter table
        string sql = """
                     SELECT *
                     FROM (Answer)
                     """;

        SqlCommand command = new SqlCommand();

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(/* anything that should be printed */);
        }
    }
}