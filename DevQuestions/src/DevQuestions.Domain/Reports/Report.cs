namespace DevQuestions.Domain.Reports;

public class Report
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; } // кто отправил жалобу
    public required Guid ReportedUserId { get; set; } // на кого отправили жалобу
    public Guid? ResolvedByUserId { get; set; } // кем решена жалоба
    public required string Reason { get; set; }
    public ReportStatus ReportStatus { get; set; } = ReportStatus.OPEN;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}