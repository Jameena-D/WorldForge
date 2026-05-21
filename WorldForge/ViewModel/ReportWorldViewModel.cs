namespace WorldForge.ViewModel
{
    public class ReportWorldViewModel
    {
        public int Id { get; set; }

        public int WorldId { get; set; }

        public string WorldName { get; set; } = string.Empty;

        public string WorldDescription { get; set; } = string.Empty;

        public string WorldType { get; set; } = string.Empty;

        public bool IsPublic { get; set; }

        public string OwnerName { get; set; } = string.Empty;

        public string ReporterName { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;

        public DateTime ReportedAt { get; set; }
    }
}
