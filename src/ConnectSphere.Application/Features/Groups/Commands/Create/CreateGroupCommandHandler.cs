using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;

namespace ConnectSphere.Application.Features.Groups.Commands.Create;

public sealed class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, long>
{
    private readonly IApplicationDbContext _context;

    private readonly ICacheInvalidator _cacheInvalidator;
    public CreateGroupCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }
    public async Task<long> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = Group.Create(request.GroupName, request.CreatedById);
        
        _context.Groups.Add(group);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        await _cacheInvalidator.InvalidateGroupAsync("Groups", cancellationToken);
        
        return group.Id;
    }
}