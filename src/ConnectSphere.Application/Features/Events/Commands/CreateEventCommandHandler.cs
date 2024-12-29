using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;

namespace ConnectSphere.Application.Features.Events.Commands
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, long>
    {
        private readonly IApplicationDbContext _context;

        private readonly ICacheInvalidator _cacheInvalidator;

        public CreateEventCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
        {
            _context = context;
            _cacheInvalidator = cacheInvalidator;
        }

        public async Task<long> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = Event.Create(request.EventName, request.Description, request.Location, request.EventDate);

            _context.Events.Add(eventEntity);
            
            await _context.SaveChangesAsync(cancellationToken);

            await _cacheInvalidator.InvalidateGroupAsync("Events", cancellationToken);

            return eventEntity.Id; // Etkinliğin ID'sini döndür
        }
    }
} 