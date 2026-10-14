using Microsoft.Data.SqlClient;
using TaskMaster.Application.Abstractions.Persistence;
using TaskMaster.Domain;

namespace TaskMaster.Infrastructure.Persistence.SqlServer;

public sealed class SqlServerTaskMasterRepository :
    IUserRepository,
    IQuestionRepository,
    IAnswerRepository,
    IDatabaseInitializer
{
    private readonly string _connectionString;

    public SqlServerTaskMasterRepository(string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        _connectionString = connectionString;
    }

    public void Initialize()
    {
        using SqlConnection connection = CreateConnection();
        connection.Open();

        const string sql = """
            IF OBJECT_ID(N'dbo.Users', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.Users
                (
                    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Users PRIMARY KEY,
                    Name nvarchar(max) NOT NULL,
                    LastName nvarchar(max) NOT NULL,
                    UserName nvarchar(20) COLLATE Latin1_General_100_CI_AS NOT NULL,
                    Password nvarchar(512) NOT NULL,
                    Level int NOT NULL CONSTRAINT DF_Users_Level DEFAULT (0),
                    Score int NOT NULL CONSTRAINT DF_Users_Score DEFAULT (0),
                    Role int NOT NULL,
                    CONSTRAINT UQ_Users_UserName UNIQUE (UserName),
                    CONSTRAINT CK_Users_Level CHECK (Level BETWEEN 0 AND 2),
                    CONSTRAINT CK_Users_Role CHECK (Role BETWEEN 0 AND 1)
                );
            END;

            IF OBJECT_ID(N'dbo.Questions', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.Questions
                (
                    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Questions PRIMARY KEY,
                    Description nvarchar(max) NOT NULL,
                    Grade int NOT NULL,
                    Difficulty int NOT NULL,
                    CreatedAt datetime2 NOT NULL,
                    UpdatedAt datetime2 NOT NULL,
                    CONSTRAINT CK_Questions_Grade CHECK (Grade BETWEEN 1 AND 5),
                    CONSTRAINT CK_Questions_Difficulty CHECK (Difficulty BETWEEN 0 AND 2)
                );
            END;

            IF OBJECT_ID(N'dbo.Answers', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.Answers
                (
                    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Answers PRIMARY KEY,
                    UserId int NOT NULL,
                    QuestionId int NOT NULL,
                    Code nvarchar(max) NOT NULL,
                    ApprovalStatus int NOT NULL CONSTRAINT DF_Answers_Status DEFAULT (0),
                    Grade int NOT NULL CONSTRAINT DF_Answers_Grade DEFAULT (-1),
                    GoalGrade int NOT NULL,
                    CreatedDate datetime2 NOT NULL,
                    Description nvarchar(max) NOT NULL,
                    CONSTRAINT FK_Answers_Users FOREIGN KEY (UserId)
                        REFERENCES dbo.Users(Id) ON DELETE CASCADE,
                    CONSTRAINT FK_Answers_Questions FOREIGN KEY (QuestionId)
                        REFERENCES dbo.Questions(Id) ON DELETE CASCADE,
                    CONSTRAINT UQ_Answers_UserQuestion UNIQUE (UserId, QuestionId),
                    CONSTRAINT CK_Answers_Status CHECK (ApprovalStatus BETWEEN 0 AND 2),
                    CONSTRAINT CK_Answers_Grade CHECK (Grade BETWEEN -1 AND 5),
                    CONSTRAINT CK_Answers_GoalGrade CHECK (GoalGrade BETWEEN 1 AND 5)
                );
            END;

            IF EXISTS
            (
                SELECT 1
                FROM sys.key_constraints
                WHERE parent_object_id = OBJECT_ID(N'dbo.Answers')
                  AND name = N'UQ_Answers_AnswerId'
            )
            BEGIN
                ALTER TABLE dbo.Answers DROP CONSTRAINT UQ_Answers_AnswerId;
            END;

            IF COL_LENGTH(N'dbo.Answers', N'AnswerId') IS NOT NULL
            BEGIN
                ALTER TABLE dbo.Answers DROP COLUMN AnswerId;
            END;
            """;

        using SqlCommand command = new(sql, connection);
        command.ExecuteNonQuery();
    }

    public bool ExistsByUsername(string username)
    {
        using SqlConnection connection = CreateConnection();
        connection.Open();

        using SqlCommand command = new(
            "SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Users WHERE UserName = @UserName) THEN 1 ELSE 0 END",
            connection);
        command.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 20).Value = username;
        return (int)command.ExecuteScalar()! == 1;
    }

    public bool Add(User user)
    {
        using SqlConnection connection = CreateConnection();
        connection.Open();

        const string sql = """
            INSERT INTO dbo.Users (Name, LastName, UserName, Password, Level, Score, Role)
            VALUES (@Name, @LastName, @UserName, @Password, @Level, @Score, @Role);
            """;
        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, -1).Value = user.Name;
        command.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, -1).Value = user.LastName;
        command.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 20).Value = user.UserName;
        command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 512).Value = user.Password;
        command.Parameters.Add("@Level", System.Data.SqlDbType.Int).Value = (int)user.Level;
        command.Parameters.Add("@Score", System.Data.SqlDbType.Int).Value = user.Score;
        command.Parameters.Add("@Role", System.Data.SqlDbType.Int).Value = (int)user.Role;
        return command.ExecuteNonQuery() == 1;
    }

    public User? GetByUsername(string username)
    {
        using SqlConnection connection = CreateConnection();
        connection.Open();

        using SqlCommand command = new(
            """
            SELECT Role, Name, LastName, UserName, Password, Level, Score
            FROM dbo.Users
            WHERE UserName = @UserName;
            """,
            connection);
        command.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 20).Value = username;
        using SqlDataReader reader = command.ExecuteReader();
        return reader.Read()
            ? new User(
                (Role)reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                (Level)reader.GetInt32(5),
                reader.GetInt32(6))
            : null;
    }

    public int Update(string username, string field, string value)
    {
        string column = field switch
        {
            "Name" => "Name",
            "LastName" => "LastName",
            "UserName" => "UserName",
            "Password" => "Password",
            _ => throw new ArgumentException("Unsupported user field.", nameof(field))
        };

        using SqlConnection connection = CreateConnection();
        connection.Open();

        using SqlCommand command = new(
            $"UPDATE dbo.Users SET [{column}] = @Value WHERE UserName = @UserName",
            connection);
        int valueSize = column switch
        {
            "Name" or "LastName" => -1,
            "Password" => 512,
            _ => 20
        };
        command.Parameters.Add("@Value", System.Data.SqlDbType.NVarChar, valueSize)
            .Value = value;
        command.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 20).Value = username;
        return command.ExecuteNonQuery();
    }

    public bool Delete(string username)
    {
        using SqlConnection connection = CreateConnection();
        connection.Open();

        using SqlCommand command = new(
            "DELETE FROM dbo.Users WHERE UserName = @UserName",
            connection);
        command.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 20).Value = username;
        return command.ExecuteNonQuery() == 1;
    }

    public void Add(Question question)
    {
        using SqlConnection connection = CreateConnection();
        connection.Open();

        const string sql = """
            INSERT INTO dbo.Questions (Description, Grade, Difficulty, CreatedAt, UpdatedAt)
            VALUES (@Description, @Grade, @Difficulty, @CreatedAt, @UpdatedAt);
            """;
        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, -1).Value = question.Description;
        command.Parameters.Add("@Grade", System.Data.SqlDbType.Int).Value = question.Grade;
        command.Parameters.Add("@Difficulty", System.Data.SqlDbType.Int).Value = (int)question.Difficulty;
        command.Parameters.Add("@CreatedAt", System.Data.SqlDbType.DateTime2).Value = question.CreatedAt;
        command.Parameters.Add("@UpdatedAt", System.Data.SqlDbType.DateTime2).Value = question.UpdatedAt;
        command.ExecuteNonQuery();
    }

    public List<Question> GetAll()
    {
        using SqlConnection connection = CreateConnection();
        connection.Open();

        using SqlCommand command = new(
            """
            SELECT Id, Description, Grade, Difficulty, CreatedAt, UpdatedAt
            FROM dbo.Questions
            ORDER BY Id;
            """,
            connection);
        using SqlDataReader reader = command.ExecuteReader();

        List<Question> questions = [];
        while (reader.Read())
        {
            questions.Add(new Question(
                reader.GetString(1),
                reader.GetInt32(2),
                (Difficulty)reader.GetInt32(3),
                reader.GetInt32(0),
                reader.GetDateTime(4),
                reader.GetDateTime(5)));
        }

        return questions;
    }

    public List<Question> GetAvailable(Difficulty difficulty, string username)
    {
        using SqlConnection connection = CreateConnection();
        connection.Open();

        using SqlCommand command = new(
            """
            SELECT q.Id, q.Description, q.Grade, q.Difficulty, q.CreatedAt, q.UpdatedAt
            FROM dbo.Questions AS q
            WHERE q.Difficulty <= @Difficulty
              AND NOT EXISTS
              (
                  SELECT 1
                  FROM dbo.Answers AS a
                  INNER JOIN dbo.Users AS u ON u.Id = a.UserId
                  WHERE a.QuestionId = q.Id AND u.UserName = @UserName
              )
            ORDER BY q.Id;
            """,
            connection);
        command.Parameters.Add("@Difficulty", System.Data.SqlDbType.Int).Value = (int)difficulty;
        command.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 20).Value = username;
        using SqlDataReader reader = command.ExecuteReader();

        List<Question> questions = [];
        while (reader.Read())
        {
            questions.Add(new Question(
                reader.GetString(1),
                reader.GetInt32(2),
                (Difficulty)reader.GetInt32(3),
                reader.GetInt32(0),
                reader.GetDateTime(4),
                reader.GetDateTime(5)));
        }

        return questions;
    }

    public int Update(int id, string field, object value)
    {
        string column = field switch
        {
            "Description" => "Description",
            "Difficulty" => "Difficulty",
            "Grade" => "Grade",
            _ => throw new ArgumentException("Unsupported question field.", nameof(field))
        };

        using SqlConnection connection = CreateConnection();
        connection.Open();

        using SqlCommand command = new(
            $"UPDATE dbo.Questions SET [{column}] = @Value, UpdatedAt = @UpdatedAt WHERE Id = @Id",
            connection);
        object databaseValue = value is Difficulty difficulty ? (int)difficulty : value;
        SqlParameter valueParameter = column == "Description"
            ? command.Parameters.Add("@Value", System.Data.SqlDbType.NVarChar, -1)
            : command.Parameters.Add("@Value", System.Data.SqlDbType.Int);
        valueParameter.Value = databaseValue;
        command.Parameters.Add("@UpdatedAt", System.Data.SqlDbType.DateTime2).Value = DateTime.Now;
        command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
        return command.ExecuteNonQuery();
    }

    public bool Delete(int id)
    {
        using SqlConnection connection = CreateConnection();
        connection.Open();

        using SqlCommand command = new(
            "DELETE FROM dbo.Questions WHERE Id = @Id",
            connection);
        command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
        return command.ExecuteNonQuery() == 1;
    }

    public void Add(Answer answer)
    {
        using SqlConnection connection = CreateConnection();
        connection.Open();

        const string sql = """
            INSERT INTO dbo.Answers
                (UserId, QuestionId, Code, ApprovalStatus, Grade, GoalGrade, CreatedDate, Description)
            SELECT u.Id, @QuestionId, @Code, @ApprovalStatus, @Grade, @GoalGrade, @CreatedDate, @Description
            FROM dbo.Users AS u
            WHERE u.UserName = @UserName;
            """;
        using SqlCommand command = new(sql, connection);
        command.Parameters.Add("@QuestionId", System.Data.SqlDbType.Int).Value = answer.QuestionId;
        command.Parameters.Add("@Code", System.Data.SqlDbType.NVarChar, -1).Value = answer.Code;
        command.Parameters.Add("@ApprovalStatus", System.Data.SqlDbType.Int).Value = (int)answer.ApprovalStatus;
        command.Parameters.Add("@Grade", System.Data.SqlDbType.Int).Value = answer.Grade;
        command.Parameters.Add("@GoalGrade", System.Data.SqlDbType.Int).Value = answer.GoalGrade;
        command.Parameters.Add("@CreatedDate", System.Data.SqlDbType.DateTime2).Value = answer.CreatedDate;
        command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, -1).Value = answer.Description;
        command.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 20).Value = answer.UserName;

        if (command.ExecuteNonQuery() != 1)
        {
            throw new InvalidOperationException("The answer could not be saved because its user no longer exists.");
        }
    }

    public List<Answer> GetAll(string? username = null)
    {
        using SqlConnection connection = CreateConnection();
        connection.Open();

        using SqlCommand command = new(
            """
            SELECT a.Id, u.UserName, a.QuestionId, a.Code, a.GoalGrade, a.Description,
                   a.ApprovalStatus, a.CreatedDate, a.Grade
            FROM dbo.Answers AS a
            INNER JOIN dbo.Users AS u ON u.Id = a.UserId
            WHERE @UserName IS NULL OR u.UserName = @UserName
            ORDER BY a.CreatedDate, a.Id;
            """,
            connection);
        command.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 20).Value =
            (object?)username ?? DBNull.Value;
        using SqlDataReader reader = command.ExecuteReader();

        List<Answer> answers = [];
        while (reader.Read())
        {
            answers.Add(new Answer(
                reader.GetString(1),
                reader.GetInt32(2),
                reader.GetString(3),
                reader.GetInt32(4),
                reader.GetString(5),
                (State)reader.GetInt32(6),
                reader.GetDateTime(7),
                reader.GetInt32(8),
                reader.GetInt32(0)));
        }

        return answers;
    }

    public void Review(Answer answer, State status)
    {
        if (status is not (State.Approve or State.Reject))
        {
            throw new ArgumentOutOfRangeException(nameof(status), "An answer can only be approved or rejected.");
        }

        using SqlConnection connection = CreateConnection();
        connection.Open();
        using SqlTransaction transaction = connection.BeginTransaction();

        int grade = status == State.Approve ? answer.GoalGrade : 0;
        using SqlCommand updateAnswer = new(
            """
            UPDATE dbo.Answers
            SET ApprovalStatus = @Status, Grade = @Grade
            WHERE Id = @Id AND ApprovalStatus = @Pending;
            """,
            connection,
            transaction);
        updateAnswer.Parameters.Add("@Status", System.Data.SqlDbType.Int).Value = (int)status;
        updateAnswer.Parameters.Add("@Grade", System.Data.SqlDbType.Int).Value = grade;
        updateAnswer.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = answer.Id;
        updateAnswer.Parameters.Add("@Pending", System.Data.SqlDbType.Int).Value = (int)State.Pending;

        if (updateAnswer.ExecuteNonQuery() != 1)
        {
            throw new InvalidOperationException("This answer has already been reviewed or no longer exists.");
        }

        if (status == State.Approve)
        {
            using SqlCommand updateUser = new(
                """
                UPDATE dbo.Users
                SET Score = Score + @Points,
                    Level = CASE
                        WHEN Score + @Points <= 10 THEN 0
                        WHEN Score + @Points <= 20 THEN 1
                        ELSE 2
                    END
                WHERE UserName = @UserName;
                """,
                connection,
                transaction);
            updateUser.Parameters.Add("@Points", System.Data.SqlDbType.Int).Value = grade;
            updateUser.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar, 20).Value = answer.UserName;

            if (updateUser.ExecuteNonQuery() != 1)
            {
                throw new InvalidOperationException("The answer owner's score could not be updated.");
            }
        }

        transaction.Commit();
    }

    private SqlConnection CreateConnection() => new(_connectionString);
}
