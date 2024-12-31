using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectSphere.Application.Features.RoomTags.Commands
{
    public class CreateRoomTagCommandHandler : IRequestHandler<CreateRoomTagCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheInvalidator _cacheInvalidator;

        public CreateRoomTagCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
        {
            _context = context;
            _cacheInvalidator = cacheInvalidator;
        }

        public async Task<long> Handle(CreateRoomTagCommand request, CancellationToken cancellationToken)
        {
            var roomTag = RoomTag.Create(request.RoomId, request.Tag, null); // ChatRoom referansını burada ayarlayın

            _context.RoomTags.Add(roomTag);
            await _context.SaveChangesAsync(cancellationToken);
            await _cacheInvalidator.InvalidateGroupAsync("RoomTags", cancellationToken);

            return roomTag.Id; // Oda etiketinin ID'sini döndür
        }
    }
} 