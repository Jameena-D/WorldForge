using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApi.Models
{
    public class ReportWorld
    {
        public int Id { get; set; }

        public int WorldId { get; set; }

        [ForeignKey(nameof(WorldId))]
        public World World { get; set; } = null!;

        public string ReporterUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(ReporterUserId))]
        public Users ReporterUser { get; set; } = null!;

        public string Reason { get; set; } = string.Empty;

        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;

        public bool IsHandled { get; set; } = false;
    }
}
