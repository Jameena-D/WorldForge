namespace WorldForge.ViewModel
{
    public class PublicWorldViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? WorldType { get; set; }
        public bool IsPublic { get; set; }
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
    }

    public class PublicWorldDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? WorldType { get; set; }
        public bool IsPublic { get; set; }
        public List<WorldSectionInputViewModel> Sections { get; set; } = new();
        public List<CommentViewModel> Comments { get; set; } = new();
    }
}
