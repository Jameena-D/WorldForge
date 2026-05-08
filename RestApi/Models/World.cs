namespace RestApi.Models
{
    public class World
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //image

        public WorldTypeEnum WorldType { get; set; } 
        public bool IsPublic { get; set; }  
        public List<WorldSection> Sections { get; set; } = new List<WorldSection>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
