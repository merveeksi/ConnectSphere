using MediatR;

namespace ConnectSphere.Application.Features.RoomCategories.Commands
{
    public sealed record CreateRoomCategoryCommand(string Name, string Description) : IRequest<long>;
} 