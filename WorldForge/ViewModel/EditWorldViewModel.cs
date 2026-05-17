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
    public string Title { get; set; } = string.Empty;
    public List<WorldBlockInputViewModel> Blocks { get; set; } = new();
}

public class WorldBlockInputViewModel
{
    public int Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}

