using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectSphere.Application.Features.Posts.Commands
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheInvalidator _cacheInvalidator;

        public CreatePostCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
        {
            _context = context;
            _cacheInvalidator = cacheInvalidator;
        }

        public async Task<long> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                AuthorId = request.AuthorId,
                Type = request.Type,
                IsActive = request.IsActive,
                EventId = request.EventId,
                GroupId = request.GroupId,
                PageId = request.PageId
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync(cancellationToken);
            await _cacheInvalidator.InvalidateGroupAsync("Posts", cancellationToken);
            return post.Id; 
        }
    }
} 