using MediatR;
using ConnectSphere.Domain.Enum;

namespace ConnectSphere.Application.Features.Posts.Commands
{
    public sealed record CreatePostCommand(
        string Title, 
        string Content, 
        long AuthorId, 
        PostType Type, 
        bool IsActive = true, // Varsayılan değer true
        long? EventId = null, 
        long? GroupId = null, 
        long? PageId = null) : IRequest<long>;
} 