using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Users.Queries.GetById;

public sealed class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserGetByIdDto>
{
    private readonly IApplicationDbContext _context;

    public GetByIdUserQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<UserGetByIdDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Select(x => new UserGetByIdDto(x.Id, x.Username, x.FullName, x.Email, x.ProfilePictureUrl, x.PasswordHash, x.Role, x.CreatedAt, x.IsActive, x.LastLoginAt)
            {
                Id = 0,
                Username = null,
                Email = null,
                PasswordHash = null
            })
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        return user!;
    }
}