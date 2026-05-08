namespace RestApi.Models
{
    public class WorldSection
    {
        public int Id { get; set; }
        public int WorldId { get; set; }
        public World? World { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<WorldBlock> Blocks { get; set; } = new List<WorldBlock>();
    }
}
