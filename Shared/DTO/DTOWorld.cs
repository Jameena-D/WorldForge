using Shared.Enum;

namespace Shared.DTO
{
    public class DTOWorld
    {
        public class CreateWorldRequest
        {
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public WorldTypeEnum WorldType { get; set; }
            public bool IsPublic { get; set; }
            public string UserId { get; set; } = string.Empty;
        }

        public class UpdateWorldRequest
        {
            public int Id { get; set; }
            public string UserId { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public WorldTypeEnum WorldType { get; set; }
            public bool IsPublic { get; set; }

            public List<WorldSectionInputViewModel>? Sections { get; set; }
        }

        public class WorldSectionInputViewModel
        {
            public string Title { get; set; } = string.Empty;
            public List<WorldBlockInputViewModel> Blocks { get; set; } = new List<WorldBlockInputViewModel>();
        }

        public class WorldBlockInputViewModel
        {
            public string? Name { get; set; }
            public string? Content { get; set; }
        }
    }
}
