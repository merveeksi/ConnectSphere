using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Users.Queries.GetAll;

public sealed class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserGetAllDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllUserQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<UserGetAllDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Users.AsQueryable();

        query = query.Where(x => x.UserName.Value.ToLower() == request.UserName.Value.ToLower());

        if (request.FullName != null)
            query = query.Where(x => x.FullName == request.FullName);   // ToLower()'ı buraya ekleyemedim.

        if (request.Email != null)
            query = query.Where(x => x.Email.Value.ToLower().Contains(request.Email.Value.ToLower()));

        if (request.PasswordHash != null)
            query = query.Where(x => x.PasswordHash.Value.ToLower().Contains(request.PasswordHash.Value.ToLower()));

        if (!string.IsNullOrEmpty(request.ProfilePictureUrl))
            query = query.Where(x => x.ProfilePictureUrl.ToLower().Contains(request.ProfilePictureUrl.ToLower()));

        if (request.CreatedAt.HasValue)
            query = query.Where(x => x.CreatedAt.Date == request.CreatedAt.Value.Date);

        if (request.LastLoginAt.HasValue)
            query = query.Where(x => x.LastLoginAt.HasValue && x.LastLoginAt.Value.Date == request.LastLoginAt.Value.Date);

        if (!string.IsNullOrEmpty(request.Role))
            query = query.Where(x => x.Role.ToLower().Contains(request.Role.ToLower()));

        if (request.IsActive.HasValue)
            query = query.Where(x => x.IsActive == request.IsActive.Value);

        return query
            .AsNoTracking()
            .Select(x => new UserGetAllDto(x.Id, x.UserName, x.FullName, x.Email, x.ProfilePictureUrl, x.PasswordHash, x.Role, x.CreatedAt, x.IsActive, x.LastLoginAt)
            {
                Id = 0,
                UserName = null,
                Email = null,
                PasswordHash = null
            })
            .ToListAsync(cancellationToken);
    }
}