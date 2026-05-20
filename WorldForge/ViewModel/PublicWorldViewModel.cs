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
        public string UserName { get; set; } = string.Empty;
    }
}
