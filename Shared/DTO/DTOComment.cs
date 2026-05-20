using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTO
{
    public class DTOComment
    {
        public class CommentDto
        {
            public int Id { get; set; }
            public string Content { get; set; } = string.Empty;

            public string UserId { get; set; } = string.Empty;
            public string OwnerName { get; set; } = string.Empty; 
            public DateTime CreatedAt { get; set; }
        }

        public class CreateCommentDto
        {
            public string Content { get; set; } = string.Empty;
            public int WorldId { get; set; }
            public string UserId { get; set; } = string.Empty; 
        }
    }
}
