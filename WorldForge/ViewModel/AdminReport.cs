namespace WorldForge.ViewModel
{
    public class AdminReport
    {
        public int CommentId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string CommentContent { get; set; } = string.Empty;
        public string CommenterFullName { get; set; } = string.Empty;
        public string ReporterFullName { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; }
    }
}
