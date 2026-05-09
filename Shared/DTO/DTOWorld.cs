using Shared.Enum;

namespace Shared.DTO
{
    public class DTOWorld
    {
        public class CreateWorldRequest
        {
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public WorldTypeEnum WorldType { get; set; }
            public bool IsPublic { get; set; }
        }
    }
}
