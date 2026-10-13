namespace DataBase;
using Microsoft.Data.SqlClient;
using UserModel;

public enum DataType { Users, Questions, Answers }

public static class Database
{
    private static string GetConnectionString(string username, string password)
    {
        return $"Server=192.168.1.138,1433;Database=TaskMaster;User Id={username};Password={password};TrustServerCertificate=True;";
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

    public static List<User>? LoadUser()
    {
        try
        {
            (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

            string connectionString = GetConnectionString(username, password);
            using SqlConnection connection = new(connectionString);
            connection.Open();
            
            string sql = """
                SELECT * FROM Users
            """;

            SqlCommand command = new(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            List<User> users = new();
            while (reader.Read())
            {
                User user = new(
                    (Role)reader["Role"],
                    reader["Name"].ToString()!,
                    reader["LastName"].ToString()!,
                    reader["UserName"].ToString()!,
                    reader["Password"].ToString()!,
                    (Level)reader["Level"],
                    Convert.ToInt32(reader["Score"])
                );

                users.Add(user);
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

    public static int UpdateUser(string userName, string field, string data)
    {
        (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

        string connectionString = GetConnectionString(username, password);
        using SqlConnection connection = new(connectionString);
        connection.Open();

        string sql;
        sql = $"""
            UPDATE Users
            SET [{field}] = @Value
            WHERE UserName = @UserName
        """;

        using SqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@Value", data);
        command.Parameters.AddWithValue("@UserName", userName);
        int rowAffected = command.ExecuteNonQuery();

        return rowAffected;
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