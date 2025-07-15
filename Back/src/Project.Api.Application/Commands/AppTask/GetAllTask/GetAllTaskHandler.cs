using AutoMapper;
using Flunt.Notifications;
using Project.Api.Application.Configuration.Events;
using Project.Api.Application.Configuration.Queries;
using Project.Api.Domain.Dto;
using Project.Api.Domain.Repositories;

namespace Project.Api.Application.Commands.AppTask
{
    public class GetAllTaskHandler : Notifiable, IQueryHandler<GetAllTaskQuery>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public GetAllTaskHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<IEvent> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var baseEntity = await _taskRepository.GetAll();

                var result = _mapper.Map<IEnumerable<TaskDto>>(baseEntity);

                return new ResultEvent(true, result != null && result.Any() ? result : new List<TaskDto>());
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}