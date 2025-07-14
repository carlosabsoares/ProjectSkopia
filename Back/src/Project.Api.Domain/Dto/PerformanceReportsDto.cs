using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Api.Domain.Dto
{
    public class PerformanceReportsDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public long NumberTasksCompleted { get; set; } = 0;
    }
}
