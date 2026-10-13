namespace DataBase.Question;

using TaskMaster.DataModel;
using Microsoft.Data.SqlClient;
using System.Collections;

class QuestionDatabase
{
    private static void SQLConnection(out SqlConnection Connection)
    {
        string username = SystemValidation.System.GetSqlAuthenticationInfo().Item1;
        string password = SystemValidation.System.GetSqlAuthenticationInfo().Item2;
        string ConnectionString = GetConnectionString(username, password);
        using SqlConnection connection = new(ConnectionString);
        Connection = connection;
    }
    private static string GetConnectionString(string username, string password)
    {
        return $"Server=192.168.1.138,1433;Database=TaskMaster;User Id={username};Password={password};TrustServerCertificate=True;";
    }

    public static void CreateQuestion(Question question)
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
        // ---------- Dfining the lists ----------
        List<string> questions = new();
        List<int> QuestionIds = new();

        try
        {
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
                // ---------- Adding question to list ----------
                string? description = dataReader["Description"].ToString()?.Length > 20 ?
                dataReader["Description"].ToString()?[..20] + "..." : dataReader["Description"].ToString();
                Difficulty difficulty = (Difficulty)Convert.ToInt32(dataReader["Difficulty"]);
                string questionItem = $"""
        ┌─────────────────────────────
          │ Task
          ├─────────────────────────────
          │ Description : {description}
          │ Difficulty  : {difficulty}
          │ Score       : {dataReader["Grade"]}
          └─────────────────────────────
        """;
                questions.Add(questionItem);
                QuestionIds.Add(Convert.ToInt32(dataReader["Id"]));
            }
        }
        catch (InvalidOperationException e)
        {
            SystemValidation.System.PrintColorizeMessage(e.Message, ConsoleColor.Red);
        }
        Ids = QuestionIds;
        return questions.ToArray();
    }
    public static void UpdateQuestion<T>(int id, T value, int parameter)
    {
        try
        {
            string username = SystemValidation.System.GetSqlAuthenticationInfo().Item1;
            string password = SystemValidation.System.GetSqlAuthenticationInfo().Item2;
            string ConnectionString = GetConnectionString(username, password);
            using SqlConnection connection = new(ConnectionString);
            connection.Open();
            string command = """
        UPDATE Questions
        SET @Parameter = @Value
        WHERE Id = @Id
        """;
            SqlCommand sqlCommand = new(command, connection);
            sqlCommand.Parameters.AddWithValue("Value", value);
            sqlCommand.Parameters.AddWithValue("Id", id);

            switch (parameter)
            {
                case 0:
                    sqlCommand.Parameters.AddWithValue("@Parameter", "Description");
                    break;
                case 1:
                    sqlCommand.Parameters.AddWithValue("@Parameter", "Difficulty");
                    break;
                case 2:
                    sqlCommand.Parameters.AddWithValue("@Parameter", "Grade");
                    break;
            }
            int RowsAffected = sqlCommand.ExecuteNonQuery();
        }
        catch (InvalidOperationException e)
        {
            SystemValidation.System.PrintColorizeMessage(e.Message, ConsoleColor.Red);
        }
    }
    public static void DeleteQuestion(int id)
    {
        try
        {
            string username = SystemValidation.System.GetSqlAuthenticationInfo().Item1;
            string password = SystemValidation.System.GetSqlAuthenticationInfo().Item2;
            string ConnectionString = GetConnectionString(username, password);
            using SqlConnection connection = new(ConnectionString);
            connection.Open();
            string command = """
        DELETE Questions
        WHERE Id = @Id
        """;
            SqlCommand sqlCommand = new(command, connection);
            sqlCommand.Parameters.AddWithValue("Id", id);
            int RowsAffected = sqlCommand.ExecuteNonQuery();
        }
        catch (InvalidOperationException e)
        {
            SystemValidation.System.PrintColorizeMessage(e.Message, ConsoleColor.Red);
        }
    }
    public static List<Question> GetQuestionsAsList()
    {
        List<Question> questions = new();
        try
        {
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
                string Description = dataReader["Description"].ToString()!;
                Difficulty difficulty = (Difficulty)Convert.ToInt32(dataReader["Difficulty"]);
                int grade = Convert.ToInt32(dataReader["Grade"]);
                int id = Convert.ToInt32(dataReader["Id"]);
                Question question = new(Description, grade, difficulty, id);
                questions.Add(question);
            }
        }
        catch (InvalidOperationException e)
        {
            SystemValidation.System.PrintColorizeMessage(e.Message, ConsoleColor.Red);
        }
        return questions;
    }
}