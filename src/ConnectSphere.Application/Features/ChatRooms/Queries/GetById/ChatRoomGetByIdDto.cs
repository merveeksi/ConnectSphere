namespace ConnectSphere.Application.Features.ChatRooms.Queries.GetById
{
    public record ChatRoomGetByIdDto(long Id, string ChatRoomName, string Description, long CreatorId);
} 