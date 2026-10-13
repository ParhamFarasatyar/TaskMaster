using System.IO.Pipes;
using TaskMaster.DataModel;
using Microsoft.Data.SqlClient;
using UserModel;


namespace DataBase;

public static class AnswerDatabase
{
    private static string GetConnectionString(string username, string password)
    {
        return $"Server=192.168.1.138,1433;Database=TaskMaster;User Id={username};Password={password};TrustServerCertificate=True;";
    }
    
    
    public static List<(int, int)> GetAnswers()
    {
        List<(int, int)> answers = new ();
        
        (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

        string connectionString = GetConnectionString(username, password);
        
        using SqlConnection connection = new SqlConnection(connectionString);

        connection.Open();
        
        string sql = """
                     SELECT 
                         UserId,
                         QuestionId
                     FROM Answers
                     """;

        SqlCommand command = new SqlCommand(sql, connection);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            int userId = (int)reader["UserId"];
            int questionId = (int)reader["QuestionId"];
            
            answers.Add((questionId, questionId));
        }

        return answers;
    }
    
    
    
    
    
    public static void SaveAnswer(Answer answer)
    {
        (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

        string connectionString = GetConnectionString(username, password);

        using SqlConnection connection = new SqlConnection(connectionString);

        connection.Open();
        
        string sql = """
                     INSERT INTO Answers
                     VALUES (@UserId, @QuestionId, @Code, @ApprovalStatus, @Grade, @GoalGrade, @CreatedAt)
                     """;

        using SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@UserId", answer.UserName);
        command.Parameters.AddWithValue("@QuestionId", answer.ApprovalStatus);
        command.Parameters.AddWithValue("@Code", answer.CreatedDate);
        command.Parameters.AddWithValue("@ApprovalStatus", answer.Code);
        command.Parameters.AddWithValue("@Grade", answer.Grade);
        command.Parameters.AddWithValue("@GoalGrade", answer.Grade);
        command.Parameters.AddWithValue("@CreatedAt", answer.Description);

        command.ExecuteNonQuery();

        command.Clone();
    }
    
     
     public static void LoadAnswer(string userName)
     {
         (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

         string connectionString = GetConnectionString(username, password);

         using SqlConnection connection = new SqlConnection(connectionString);

         connection.Open();

         string sql = """
                      SELECT *
                      FROM Answers
                      """;

         using SqlCommand command = new SqlCommand();

         SqlDataReader reader = command.ExecuteReader();

         while (reader.Read())
         {
             string answers = $"""
                      ┌────────────────────────────
                      │ Username: {userName}
                      │ Code: {reader["Code"]}
                      │ ApprovalStatus: {reader["ApprovalStatus"]}
                      │ Grade: {reader["Grade"]}
                      │ GoalGrade: {reader["GoalGrade"]}
                      │ CreatedAt: {reader["CreateAt"]}
                      └─────────────────────────────
              """;
         }
     }
     

     public static void UpdateApprovalStatus(int status, int answerId)
     {
         (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

         string connectionString = GetConnectionString(username, password);

         using SqlConnection connection = new SqlConnection(connectionString);

         connection.Open();

         string sql = """
                      UPDATE ANSWER
                      SET ApprovalStatus = @Status
                      WHERE Id = @AnswerId
                      """;
         
         using SqlCommand command = new SqlCommand(sql, connection);

         command.Parameters.AddWithValue("@Status", (State)status);
         command.Parameters.AddWithValue("@AnswerId", answerId);
     }
}

