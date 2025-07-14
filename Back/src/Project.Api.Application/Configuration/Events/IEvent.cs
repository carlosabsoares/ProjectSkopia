namespace Project.Api.Application.Configuration.Events
{
    public interface IEvent
    {
        bool Success { get; }
        object Data { get; }
    }
}