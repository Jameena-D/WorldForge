namespace RestApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;

        public World World { get; set; } = null!;
        public int WorldId { get; set; }

        public Users User { get; set; } = null!;
        public int UserId { get; set; }

    }
}
