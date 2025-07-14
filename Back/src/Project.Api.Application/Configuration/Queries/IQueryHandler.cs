using MediatR;
using Project.Api.Application.Configuration.Events;

namespace Project.Api.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery, IEvent> where TQuery : IQuery
    {
    }
}