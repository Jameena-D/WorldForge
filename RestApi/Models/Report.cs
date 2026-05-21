using System.ComponentModel.DataAnnotations.Schema;

namespace RestApi.Models
{
    public class Report
    {
        public int Id { get; set; }

        // Comment
        public int CommentId { get; set; }
        public Comment Comment { get; set; } = null!;

        // The user being reported
        public string ReportedUserId { get; set; } = string.Empty;
        [ForeignKey("ReportedUserId")]
        public Users ReportedUser { get; set; } = null!;

        // The user doing teh reporting
        public string ReporterUserId { get; set; } = string.Empty;
        [ForeignKey("ReporterUserId")]
        public Users ReporterUser { get; set; } = null!;

        public string Reason { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;

        // for admin
        public string Status { get; set; } = "Pending";
    }
}
