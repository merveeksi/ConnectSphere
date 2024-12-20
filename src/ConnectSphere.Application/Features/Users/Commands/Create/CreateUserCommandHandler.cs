using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;

namespace ConnectSphere.Application.Features.Users.Commands.Create;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public CreateUserCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Username, request.FullName, request.Email, request.PasswordHash, request.profilePictureUrl, request.role);
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        await _cacheInvalidator.InvalidateGroupAsync("Users", cancellationToken);
        return user.Id;
    }
} 