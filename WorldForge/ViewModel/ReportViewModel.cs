using System.ComponentModel.DataAnnotations;

namespace WorldForge.ViewModel
{
    public class ReportViewModel
    {
        public int CommentId { get; set; }

        [Required(ErrorMessage = "You must provide a reason for reporting.")]
        public string Reason { get; set; } = string.Empty;
    }
}
