namespace Project.Api.Domain.Dto
{
    public class PerformanceReportsDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public long NumberTasksCompleted { get; set; } = 0;
    }
}