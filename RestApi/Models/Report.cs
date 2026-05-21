namespace RestApi.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; } = null!;
        public string ReportedUserId { get; set; } = string.Empty;

        public Users ReportedUser { get; set; } = null!;
        public string Reason {  get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
