using TaskMaster.Domain;

namespace TaskMaster.Presentation.Cli.Menus;

internal static class AnswerFormatter
{
    public static string[] Format(IEnumerable<Answer> answers)
    {
        return answers.Select(answer =>
        {
            int score = answer.Grade == -1 ? 0 : answer.Grade;
            return $"""
                ┌────────────────────────────
                │ Description: {answer.Description}
                │ User name: {answer.UserName}
                │ Code: {answer.Code}
                │ Date/Time: {answer.CreatedDate:dd/MM/yyyy HH:mm}
                │ State: {answer.ApprovalStatus}
                │ Score: {score}
                └─────────────────────────────
                """;
        }).ToArray();
    }
}
