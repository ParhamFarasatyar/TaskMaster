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
    public static void SaveQuestion(TaskMaster.DataModel.Question question)
    {
        string username = SystemValidation.System.GetSqlAuthenticationInfo().Item1;
        string password = SystemValidation.System.GetSqlAuthenticationInfo().Item2;
        string ConnectionString = GetConnectionString(username, password);
        using SqlConnection connection = new(ConnectionString);
        connection.Open();
        string command = """
        INSERT INTO Questions
        Values(@Description, @Grade, @Difficulty, @CreatedAt, @UpdatedAt)
        """;
        SqlCommand sqlCommand = new(command, connection);
        sqlCommand.Parameters.AddWithValue("@Description", question.Description);
        sqlCommand.Parameters.AddWithValue("@Grade", question.Grade);
        sqlCommand.Parameters.AddWithValue("@Difficulty", question.Difficulty);
        sqlCommand.Parameters.AddWithValue("@CreatedAt", question.CreatedAt);
        sqlCommand.Parameters.AddWithValue("@UpdatedAt", question.UpdatedAt);
        int RowsAffected = sqlCommand.ExecuteNonQuery();
        Console.WriteLine($"{RowsAffected} rows affected");
    }

    public static string[] LoadQuestions(out List<int> Ids)
    {
        List<string> questions = new();
        List<int> QuestionIds = new();
        string username = SystemValidation.System.GetSqlAuthenticationInfo().Item1;
        string password = SystemValidation.System.GetSqlAuthenticationInfo().Item2;
        string ConnectionString = GetConnectionString(username, password);
        using SqlConnection connection = new(ConnectionString);
        connection.Open();
        string command = """
        SELECT * FROM Questions
        """;
        SqlCommand sqlCommand = new(command, connection);
        SqlDataReader dataReader = sqlCommand.ExecuteReader();

        while (dataReader.Read())
        {
            string? description = dataReader["Description"].ToString()?.Length > 20 ?
            dataReader["Description"].ToString()?[..20] + "..." : dataReader["Description"].ToString();
            Difficulty difficulty = (Difficulty)dataReader["Difficulty"];
            string questionItem = $"""
        ┌─────────────────────────────
          │ Task
          ├─────────────────────────────
          │ Description : {description}
          │ Difficulty  : {difficulty.ToString()}
          │ Score       : {dataReader["Grade"].ToString()}
          └─────────────────────────────
        """;
        questions.Add(questionItem);
        QuestionIds.Add(Convert.ToInt32(dataReader["Id"]));
        }

        Ids = QuestionIds;
        return questions.ToArray();
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