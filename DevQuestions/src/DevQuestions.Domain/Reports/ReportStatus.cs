namespace DevQuestions.Domain.Reports;

public enum ReportStatus
{
    /// <summary>
    /// Статус открыт
    /// </summary>
    OPEN,

    /// <summary>
    /// Статус в работе
    /// </summary>
    IN_PROGRESS,

    /// <summary>
    /// Статус в решен
    /// </summary>
    RESOLVED,

    /// <summary>
    /// Статус закрыт
    /// </summary>
    DISMISSED,
}