namespace WorldForge.ViewModel
{
    public class EditWorldViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WorldType { get; set; }
        public bool IsPublic { get; set; }
        public List<WorldSectionInputViewModel> Sections { get; set; } = new List<WorldSectionInputViewModel>();
    }

    public class WorldSectionInputViewModel
    {
        public string Title { get; set; } = string.Empty;

        // More than one block allowed per section
        public List<WorldBlockInputViewModel> Blocks { get; set; } = new List<WorldBlockInputViewModel>();
    }

    public class WorldBlockInputViewModel
    {
        public string Name { get; set; } = string.Empty;   // bv. Character 1, Character 2
        public string Content { get; set; } = string.Empty; // bv. beschrijving van dat blok
    }
}
