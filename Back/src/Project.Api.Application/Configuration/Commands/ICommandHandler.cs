using MediatR;
using Project.Api.Application.Configuration.Events;

namespace Project.Api.Application.Configuration.Commands
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, IEvent> where TCommand : ICommand
    {
    }
}