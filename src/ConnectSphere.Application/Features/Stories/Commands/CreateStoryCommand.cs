using ConnectSphere.Domain.Enum;
using ConnectSphere.Domain.Identity;
using MediatR;

namespace ConnectSphere.Application.Features.Stories.Commands
{
    public sealed record CreateStoryCommand(string Content, string MediaUrl, StoryType Type, DateTime ExpirationTime, long UserId, ApplicationUser user, bool IsPublic) : IRequest<long>;
} 