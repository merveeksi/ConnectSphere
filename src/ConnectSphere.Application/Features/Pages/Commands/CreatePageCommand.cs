using MediatR;

namespace ConnectSphere.Application.Features.Pages.Commands
{
    public sealed record CreatePageCommand(string Title, string Description, string Url) : IRequest<long>;
} 