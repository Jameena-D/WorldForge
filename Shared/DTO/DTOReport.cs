using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTO
{
    public class DTOReport
    {
        public int CommentId { get; set; }
        public string ReportedUserId { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class MuteUserDto
    {
        public string UserId { get; set; } = string.Empty;
    }
}
