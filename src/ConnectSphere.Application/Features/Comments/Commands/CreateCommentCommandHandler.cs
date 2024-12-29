using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectSphere.Application.Features.Comments.Commands
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, long>
    {
        private readonly IApplicationDbContext _context;

        private readonly ICacheInvalidator _cacheInvalidator;

        public CreateCommentCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
        {
            _context = context;
            _cacheInvalidator = cacheInvalidator;
        }

        public async Task<long> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment(request.PostId, null, request.UserId, null, request.Content);

            // Post ve User nesnelerini veritabanından al
            var post = await _context.Posts.FindAsync(request.PostId);
            var user = await _context.Users.FindAsync(request.UserId);

            if (post == null || user == null)
            {
                // Hata fırlat veya uygun bir işlem yap
                throw new Exception("Post or User not found.");
            }

            // İlişkileri ayarla
            comment.Post = post;
            comment.User = user;

            // Yorum nesnesini veritabanına ekle
            await _context.Comments.AddAsync(comment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return comment.Id; // Yorumun ID'sini döndür
        }
    }
} 