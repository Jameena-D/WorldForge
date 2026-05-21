using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTO
{
    public class DTOReportWorld
    {
        public int WorldId { get; set; }
        public string ReporterUserId { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }
}
