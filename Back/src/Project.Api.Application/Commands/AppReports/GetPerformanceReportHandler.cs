using Flunt.Notifications;
using Project.Api.Application.Configuration.Events;
using Project.Api.Application.Configuration.Queries;
using Project.Api.Domain.Dto;
using Project.Api.Domain.Repositories;

namespace Project.Api.Application.Commands.AppReports
{
    public class GetPerformanceReportHandler : Notifiable, IQueryHandler<GetPerformanceReportQuery>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public GetPerformanceReportHandler(ITaskRepository taskRepository,
                                           IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public async Task<IEvent> Handle(GetPerformanceReportQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var days = 30;

                var baseEntity = await _taskRepository.GetPerformanceReport(days);

                var grouped = baseEntity
                    .GroupBy(t => t.AuthorId)
                    .Select(g => new
                    {
                        UserId = g.Key,
                        NumberTasksCompleted = g.Count()
                    })
                    .ToList();

                var userIds = grouped.Select(g => g.UserId).Distinct().ToList();
                var users = await _userRepository.GetByIds(userIds);

                var result = grouped
                    .Join(users,
                        g => g.UserId,
                        u => u.Id,
                        (g, u) => new PerformanceReportsDto
                        {
                            UserId = g.UserId,
                            UserName = u.Name,
                            NumberTasksCompleted = g.NumberTasksCompleted
                        })
                    .ToList();

                return new ResultEvent(true, result.Any() ? result : new List<PerformanceReportsDto>());
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}