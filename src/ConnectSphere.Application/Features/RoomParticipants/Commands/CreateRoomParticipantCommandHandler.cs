using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectSphere.Application.Features.RoomParticipants.Commands
{
    public class CreateRoomParticipantCommandHandler : IRequestHandler<CreateRoomParticipantCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheInvalidator _cacheInvalidator;

        public CreateRoomParticipantCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
        {
            _context = context;
            _cacheInvalidator = cacheInvalidator;
        }

        public async Task<long> Handle(CreateRoomParticipantCommand request, CancellationToken cancellationToken)
        {
            var roomParticipant = RoomParticipant.Create(request.RoomId , request.UserId, request.Role);

            _context.RoomParticipants.Add(roomParticipant);

            await _context.SaveChangesAsync(cancellationToken);

            await _cacheInvalidator.InvalidateGroupAsync("RoomParticipants", cancellationToken);
            
            return roomParticipant.UserId; 
        }
    }
} 