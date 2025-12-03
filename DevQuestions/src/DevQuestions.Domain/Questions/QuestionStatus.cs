namespace DevQuestions.Domain.Questions;

public enum QuestionStatus
{
    /// <summary>
    /// Статус открыт
    /// </summary>
    OPEN,

    /// <summary>
    /// Статус в решен
    /// </summary>
    RESOLVED,
}

public static class QuestionStatusExtensions
{
    public static string ToRussianString(this QuestionStatus status) =>
        status switch
        {
            QuestionStatus.OPEN => "Открыт",
            QuestionStatus.RESOLVED => "Решен",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
}