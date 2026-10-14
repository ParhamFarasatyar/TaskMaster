using TaskMaster.Domain;

namespace TaskMaster.Presentation.Cli.Menus;

internal static class QuestionFormatter
{
    public static string[] Format(IEnumerable<Question> questions)
    {
        return questions.Select(question =>
        {
            string description = question.Description.Length > 20
                ? question.Description[..20] + "..."
                : question.Description;

            return $"""
                ┌─────────────────────────────
                  │ Task
                  ├─────────────────────────────
                  │ Description : {description}
                  │ Difficulty  : {question.Difficulty}
                  │ Score       : {question.Grade}
                └─────────────────────────────
                """;
        }).ToArray();
    }
}
