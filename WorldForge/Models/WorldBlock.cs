namespace WorldForge.Models
{
    public class WorldBlock
    {
        public int Id { get; set; }
        public int WorldSectionId { get; set; }
        public WorldSection? WorldSection { get; set; }
        public string Name { get; set; } = string.Empty;
        //add for picture later
        public string Content { get; set; } = string.Empty;
        
    }
}
