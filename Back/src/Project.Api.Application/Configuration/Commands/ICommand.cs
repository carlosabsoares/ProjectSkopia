using Flunt.Validations;
using MediatR;
using Project.Api.Application.Configuration.Events;

namespace Project.Api.Application.Configuration.Commands
{
    public interface ICommand : IRequest<IEvent>, IValidatable
    {
    }
}