namespace RestApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public World World { get; set; } = null!;
        public int WorldId { get; set; }

        public Users User { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;

    }
}
