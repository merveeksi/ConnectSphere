using MediatR;

namespace ConnectSphere.Application.Features.Comments.Commands
{
    public sealed record CreateCommentCommand(long PostId, string Post, long UserId, string User, string Content) : IRequest<long>;
} 