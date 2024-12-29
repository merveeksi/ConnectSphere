using MediatR;

namespace ConnectSphere.Application.Features.Events.Commands
{
    public sealed record CreateEventCommand(string EventName, string Description, string Location, DateTime EventDate) : IRequest<long>;
} 