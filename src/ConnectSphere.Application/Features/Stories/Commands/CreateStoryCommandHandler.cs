using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectSphere.Application.Features.Stories.Commands
{
    public class CreateStoryCommandHandler : IRequestHandler<CreateStoryCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheInvalidator _cacheInvalidator;

        public CreateStoryCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
        {
            _context = context;
            _cacheInvalidator = cacheInvalidator;
        }

        public async Task<long> Handle(CreateStoryCommand request, CancellationToken cancellationToken)
        {
            var story = Story.Create(request.Content, request.MediaUrl, request.Type, request.ExpirationTime, request.UserId, request.user);

            _context.Stories.Add(story);
            await _context.SaveChangesAsync(cancellationToken);
            await _cacheInvalidator.InvalidateGroupAsync("Stories", cancellationToken);

            return story.Id;
        }
    }
} 