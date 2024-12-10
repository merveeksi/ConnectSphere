using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;

namespace ConnectSphere.Application.Features.Groups.Commands.Create;

public sealed class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, long>
{
    private readonly IApplicationDbContext _context;

    public CreateGroupCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = Group.Create(request.GroupName, request.CreatedById);
        _context.Groups.Add(group);
        await _context.SaveChangesAsync(cancellationToken);
        return group.Id;
    }
}