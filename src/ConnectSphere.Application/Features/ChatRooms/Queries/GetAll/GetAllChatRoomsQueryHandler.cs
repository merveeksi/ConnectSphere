using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.ChatRooms.Queries.GetAll
{
    public sealed class GetAllChatRoomsQueryHandler : IRequestHandler<GetAllChatRoomsQuery, List<ChatRoomGetAllDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllChatRoomsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChatRoomGetAllDto>> Handle(GetAllChatRoomsQuery request, CancellationToken cancellationToken)
        {
            return await _context.ChatRooms
                .AsNoTracking()
                .Select(x => new ChatRoomGetAllDto(x.Id, x.ChatRoomName, x.Description, x.CreatorId))
                .ToListAsync(cancellationToken);
        }
    }
} 