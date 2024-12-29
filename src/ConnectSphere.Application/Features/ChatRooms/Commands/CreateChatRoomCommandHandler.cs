using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;

namespace ConnectSphere.Application.Features.ChatRooms.Commands
{
    public class CreateChatRoomCommandHandler : IRequestHandler<CreateChatRoomCommand, long>
    {
        private readonly IApplicationDbContext _context;
        
        private readonly ICacheInvalidator _cacheInvalidator;
        
        public CreateChatRoomCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
        {
            _context = context;
            _cacheInvalidator = cacheInvalidator;
        }
        public async Task<long> Handle(CreateChatRoomCommand request, CancellationToken cancellationToken)
        {
            var chatRoom = ChatRoom.Create(request.ChatRoomName, request.Description, request.CreatorId, request.MaxParticipants, request.IsPrivate, request.Status, request.CategoryId);
            
            _context.ChatRooms.Add(chatRoom);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            await _cacheInvalidator.InvalidateGroupAsync("ChatRooms", cancellationToken);
            
            return chatRoom.Id;
        }
    }
}