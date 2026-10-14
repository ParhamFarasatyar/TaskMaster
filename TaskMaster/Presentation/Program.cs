using Microsoft.Data.SqlClient;
using System.Text.Json;
using TaskMaster.Presentation.Cli.Menus;
using TaskMaster.Application.Abstractions.Persistence;
using TaskMaster.Application.Services;
using TaskMaster.Domain;
using TaskMaster.Infrastructure.Persistence.SqlServer;

namespace TaskMaster;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            SqlServerTaskMasterRepository repository = new(LoadConnectionString());
            repository.Initialize();

            IUserRepository users = repository;
            IQuestionRepository questions = repository;
            IAnswerRepository answers = repository;
            UserRegistrationService registrationService = new(users);
            AuthenticationService authenticationService = new(users);
            UserService userService = new(users);
            QuestionService questionService = new(questions);
            AnswerService answerService = new(answers);

            RunMenu(registrationService, authenticationService, userService, questionService, answerService);
        }
        catch (SqlException exception)
        {
            ConsoleHelper.PrintColorizeMessage(
                $"SQL Server operation failed: {exception.Message}",
                ConsoleColor.Red);
            return;
        }
        catch (InvalidOperationException exception)
        {
            ConsoleHelper.PrintColorizeMessage(exception.Message, ConsoleColor.Red);
            return;
        }
        catch (JsonException exception)
        {
            ConsoleHelper.PrintColorizeMessage(
                $"The appsettings.json configuration file is invalid: {exception.Message}",
                ConsoleColor.Red);
            return;
        }

    }

    private static string LoadConnectionString()
    {
        string settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(settingsPath))
        {
            throw new InvalidOperationException(
                $"Configuration file not found: {settingsPath}. Create appsettings.json with ConnectionStrings:TaskMaster.");
        }

        using FileStream settingsFile = File.OpenRead(settingsPath);
        using JsonDocument settings = JsonDocument.Parse(settingsFile);

        if (!settings.RootElement.TryGetProperty("ConnectionStrings", out JsonElement connectionStrings) ||
            !connectionStrings.TryGetProperty("TaskMaster", out JsonElement taskMasterConnectionString) ||
            taskMasterConnectionString.ValueKind != JsonValueKind.String ||
            string.IsNullOrWhiteSpace(taskMasterConnectionString.GetString()))
        {
            throw new InvalidOperationException(
                "Configuration file must define a non-empty ConnectionStrings:TaskMaster value.");
        }

        return taskMasterConnectionString.GetString()!;
    }

    private static void RunMenu(
        UserRegistrationService registrationService,
        AuthenticationService authenticationService,
        UserService userService,
        QuestionService questionService,
        AnswerService answerService)
    {
        while(true)
        {
            MainMenu mainMenu = new(registrationService, authenticationService);


            User? user = mainMenu.Show();
            
            if(user == null)
            {
                ConsoleHelper.PrintColorizeMessage("Goodbye!", ConsoleColor.White);

                break;
            }



            if(user.Role == Role.Intern)
            {
                InternMenu internMenu = new(questionService, answerService, userService);

                bool shouldContinue = internMenu.Show(user);
                if (!shouldContinue)
                {
                    continue;
                }
            }



            else if(user.Role == Role.Designer)
            {
                DesignerMenu designerMenu = new(questionService, answerService, userService);

                bool shouldContinue = designerMenu.Show(user);
                if (!shouldContinue)
                {
                    continue;
                }
            }
        }
    }
}