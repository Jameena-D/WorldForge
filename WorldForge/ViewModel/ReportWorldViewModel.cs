namespace WorldForge.ViewModel
{
    public class ReportWorldViewModel
    {
        public int WorldId { get; set; }
        public string WorldName { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public int ReportCount { get; set; }
        public List<ReportItemViewModel> Reports { get; set; } = new();
    }
    public class ReportItemViewModel
    {
        public int Id { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; }
        public string ReporterName { get; set; } = string.Empty;
    }
}
