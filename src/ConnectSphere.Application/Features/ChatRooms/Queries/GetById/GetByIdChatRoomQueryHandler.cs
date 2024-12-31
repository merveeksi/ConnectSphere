using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.ChatRooms.Queries.GetById
{
    public sealed class GetByIdChatRoomQueryHandler : IRequestHandler<GetByIdChatRoomQuery, ChatRoomGetByIdDto>
    {
        private readonly IApplicationDbContext _context;

        public GetByIdChatRoomQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChatRoomGetByIdDto> Handle(GetByIdChatRoomQuery request, CancellationToken cancellationToken)
        {
            var chatRoom = await _context.ChatRooms
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return chatRoom != null ? new ChatRoomGetByIdDto(chatRoom.Id, chatRoom.ChatRoomName, chatRoom.Description, chatRoom.CreatorId) : null;
        }
    }
} 