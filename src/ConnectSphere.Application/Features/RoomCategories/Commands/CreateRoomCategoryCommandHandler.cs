using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;

namespace ConnectSphere.Application.Features.RoomCategories.Commands
{
    public class CreateRoomCategoryCommandHandler : IRequestHandler<CreateRoomCategoryCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheInvalidator _cacheInvalidator;

        public CreateRoomCategoryCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
        {
            _context = context;
            _cacheInvalidator = cacheInvalidator;
        }

        public async Task<long> Handle(CreateRoomCategoryCommand request, CancellationToken cancellationToken)
        {
            var roomCategory = RoomCategory.Create(request.Name, request.Description);

            _context.RoomCategories.Add(roomCategory);
            await _context.SaveChangesAsync(cancellationToken);

            await _cacheInvalidator.InvalidateGroupAsync("RoomCategories", cancellationToken);

            return roomCategory.Id; 
        }
    }
} 