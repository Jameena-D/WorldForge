namespace WorldForge.Models
{
    public class World
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //add for image later

        public WorldTypeEnum WorldType { get; set; } 
        public bool isPublic { get; set; }  
        public List<WorldSection> Sections { get; set; } = new List<WorldSection>();
    }
}
