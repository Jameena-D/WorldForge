using Shared.Enum;

namespace WorldForge.ViewModel
{
    public class EditWorldViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WorldType { get; set; }
        public bool IsPublic { get; set; }
        public List<WorldSectionInputViewModel> Sections { get; set; } = new();
    }

    public class WorldSectionInputViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public List<WorldBlockInputViewModel> Blocks { get; set; } = new();
    }

    public class WorldBlockInputViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
    }
}

