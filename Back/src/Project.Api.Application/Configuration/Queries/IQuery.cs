using Flunt.Validations;
using MediatR;
using Project.Api.Application.Configuration.Events;

namespace Project.Api.Application.Configuration.Queries
{
    public interface IQuery : IRequest<IEvent>, IValidatable
    {
    }
}